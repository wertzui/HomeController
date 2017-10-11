using Plugins.Common;
using Plugins.Common.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MotionPlugin
{
    class MotionManager : IDisposable
    {
        // key is motion fixture name, not room name
        IDictionary<string, Motion> motions = new Dictionary<string, Motion>();
        IDictionary<string, bool> termostatStatuses = new Dictionary<string, bool>();
        static IDictionary<string, int> RelaisNumbers = new Dictionary<string, int>
        {
            {"Küche", 2 },
            {"Wohnzimmer", 3 },
            {"Bad", 4 },
            {"WC", 5 },
            {"Ankleide", 6 },
            {"Schlafzimmer", 7 },
            {"Gästezimmer", 8 },
            {"Kinderzimmer", 9 }
        };
        const string heatingActorBaseUrl = "http://192.168.0.55/digital/";
        static int targetStringLength = "TargetMotion".Length;
        static int measuredStringLength = "MeasuredMotion".Length;

        static IDictionary<string, string> motionSensorBaseUrls = new Dictionary<string, string>
        {
            //{"Küche", "http://192.168.0.227/" },
            //{"Wohnzimmer", "http://192.168.0.227/" },
            {"Bad", "http://192.168.0.228/" },
            //{"WC", "http://192.168.0.229/" },
            //{"Ankleide", "http://192.168.0.230/" },
            //{"Schlafzimmer", "http://192.168.0.231/" },
            //{"Gästezimmer", "http://192.168.0.232/" },
            //{"Kinderzimmer", "http://192.168.0.233/" }
        };

        readonly Timer measureTimer = new Timer(1000);
        readonly IHubClient hubClient;
        readonly HttpClient httpClient = new HttpClient();

        public MotionManager(IHubClient hubClient)
        {
            this.hubClient = hubClient;
            measureTimer.Elapsed += MeasureTimer_Elapsed;
            measureTimer.Start();
        }

        private async void MeasureTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await MeasureAllMotions().ConfigureAwait(false);
        }

        internal Task UpdateTargetMotions(IEnumerable<Channel> channels)
        {
            var tasks = channels.Select(c => UpdateTargetMotion(c)).ToList();
            return Task.WhenAll(tasks);
        }

        private async Task UpdateTargetMotion(Channel channel)
        {
            var motionName = GetFixtureNameFromTargetChannel(channel.Name);
            var savedMotion = GetOrAddMotion(motionName);

            Console.WriteLine($"Updating temp - old: {savedMotion.TargetMotion.Value}, new: {channel.Value}");
            savedMotion.TargetMotion.Value = channel.Value;

            await UpdateTermostatState(savedMotion).ConfigureAwait(false);
        }

        private static string GetFixtureNameFromTargetChannel(string channelName) => channelName.Substring(targetStringLength);


        private Motion GetOrAddMotion(string motionName)
        {
            Motion motion;
            if (!motions.TryGetValue(motionName, out motion))
            {
                motion = new Motion
                {
                    MeasuredMotion = new Channel { Name = "MeasuredMotion" + motionName, Type = "Range", CanBeModified = false, Target = "Motion" },
                    TargetMotion = new Channel { Name = "TargetMotion" + motionName, Type = "Range", CanBeModified = true, Target = "Motion" },
                    Name = motionName
                };
                motions[motionName] = motion;
            }

            return motion;
        }

        private async Task UpdateMeasuredMotion(string name, double motion, double humidity)
        {
            var savedMotion = GetOrAddMotion(name);

            // no perceived motion for now as the heat index works only above 26°C
            //var perceivedMotion = HeatIndexCalulcator.CalculateHeatIndex(motion, humidity);
            Console.WriteLine($"Updating measured temp - old: {savedMotion.MeasuredMotion.Value}, new: {motion}");

            if (savedMotion.MeasuredMotion.Value != motion)
            {
                savedMotion.MeasuredMotion.Value = motion;

                await UpdateTermostatState(savedMotion).ConfigureAwait(false);
                await UpdateFixtureRegister(savedMotion).ConfigureAwait(false);
            }
        }

        private Task UpdateFixtureRegister(Motion fixture)
             => hubClient.SendAsync(new[] { fixture.MeasuredMotion }, EventBus.Messaging.MethodType.Update, "FixtureRegister");

        private Task MeasureAllMotions()
        {
            var tasks = motionSensorBaseUrls.Keys.Select(k => MeasureMotion(k)).ToList();
            return Task.WhenAll(tasks);
        }

        private async Task MeasureMotion(string name)
        {
            try
            {
                await httpClient.GetAsync(GetMotionSensorUpdateUrl(name)).ConfigureAwait(false);
                var result = await httpClient.GetAsync(GetMotionSensorMeasureUrl(name)).ConfigureAwait(false);
                var measured = await result.Content.ReadAsAsync<MotionMeasureResult>().ConfigureAwait(false);
                await UpdateMeasuredMotion(name, measured.Variables.RealMotion, measured.Variables.RealHumidity).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to measure motion {name}. {e}");
            }
        }
        private static string GetMotionSensorMeasureUrl(string name)
            => $"{motionSensorBaseUrls[name]}";
        private static string GetMotionSensorUpdateUrl(string name)
            => GetMotionSensorMeasureUrl(name) + "update";

        Task UpdateTermostatState(Motion fixture)
        {
            if (fixture.MeasuredMotion.Value < fixture.TargetMotion.Value)
                return TryOpenTermostat(fixture.Name);
            else
                return TryCloseTermostat(fixture.Name);
        }

        private async Task TryOpenTermostat(string name)
        {
            Console.WriteLine($"Trying to open termonstat {name}");
            var isOpen = false;
            termostatStatuses.TryGetValue(name, out isOpen);
            Console.WriteLine($"Current status is {isOpen}");
            if (!isOpen)
            {
                // send open command
                var url = GetHeatingActorUrl(name, true);
                Console.WriteLine($"Opening termostat with URL {url}");
                await httpClient.GetAsync(url).ConfigureAwait(false);
                termostatStatuses[name] = true;
            }
        }

        private async Task TryCloseTermostat(string name)
        {
            Console.WriteLine($"Trying to close termonstat {name}");
            var isOpen = false;
            termostatStatuses.TryGetValue(name, out isOpen);
            Console.WriteLine($"Current status is {isOpen}");
            if (isOpen)
            {
                // send close command
                var url = GetHeatingActorUrl(name, false);
                Console.WriteLine($"Closing termostat with URL {url}");
                await httpClient.GetAsync(url).ConfigureAwait(false);
                termostatStatuses[name] = false;
            }
        }

        private static string GetHeatingActorUrl(string name, bool targetTermostatStatus)
            => $"{heatingActorBaseUrl}{RelaisNumbers[name]}/{(targetTermostatStatus ? 0 : 1)}";

        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    httpClient?.Dispose();
                    measureTimer?.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
