using Plugins.Common;
using Plugins.Common.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TemperaturePlugin
{
    class TemperatureManager : IDisposable
    {
        // key is temperature fixture name, not room name
        IDictionary<string, Temperature> temperatures = new Dictionary<string, Temperature>();
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
        static int targetStringLength = "TargetTemperature".Length;
        static int measuredStringLength = "MeasuredTemperature".Length;

        static IDictionary<string, string> temperatureSensorBaseUrls = new Dictionary<string, string>
        {
            {"Küche", "http://192.168.0.220/" },
            {"Wohnzimmer", "http://192.168.0.220/" },
            {"Bad", "http://192.168.0.221/" },
            {"WC", "http://192.168.0.222/" },
            {"Ankleide", "http://192.168.0.223/" },
            //{"Schlafzimmer", "http://192.168.0.0/" },
            {"Gästezimmer", "http://192.168.0.225/" },
            {"Kinderzimmer", "http://192.168.0.226/" }
        };

        readonly Timer measureTimer = new Timer(60000);
        readonly IHubClient hubClient;
        readonly HttpClient httpClient = new HttpClient();

        public TemperatureManager(IHubClient hubClient)
        {
            this.hubClient = hubClient;
            measureTimer.Elapsed += MeasureTimer_Elapsed;
            measureTimer.Start();
        }

        private async void MeasureTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await MeasureAllTemperatures().ConfigureAwait(false);
        }

        internal Task UpdateTargetTemperatures(IEnumerable<Channel> channels)
        {
            var tasks = channels.Select(c => UpdateTargetTemperature(c)).ToList();
            return Task.WhenAll(tasks);
        }

        private async Task UpdateTargetTemperature(Channel channel)
        {
            var temperatureName = GetFixtureNameFromTargetChannel(channel.Name);
            var savedTemperature = GetOrAddTemperature(temperatureName);

            Console.WriteLine($"Updating temp - old: {savedTemperature.TargetTemperature.Value}, new: {channel.Value}");
            savedTemperature.TargetTemperature.Value = channel.Value;

            await UpdateTermostatState(savedTemperature).ConfigureAwait(false);
        }

        private static string GetFixtureNameFromTargetChannel(string channelName) => channelName.Substring(targetStringLength);


        private Temperature GetOrAddTemperature(string temperatureName)
        {
            Temperature temperature;
            if (!temperatures.TryGetValue(temperatureName, out temperature))
            {
                temperature = new Temperature
                {
                    MeasuredTemperature = new Channel { Name = "MeasuredTemperature" + temperatureName, Type = "Range", CanBeModified = false, Target = "Temperature" },
                    TargetTemperature = new Channel { Name = "TargetTemperature" + temperatureName, Type = "Range", CanBeModified = true, Target = "Temperature" },
                    Name = temperatureName
                };
                temperatures[temperatureName] = temperature;
            }

            return temperature;
        }

        private async Task UpdateMeasuredTemperature(string name, double temperature, double humidity)
        {
            var savedTemperature = GetOrAddTemperature(name);

            // no perceived temperature for now as the heat index works only above 26°C
            //var perceivedTemperature = HeatIndexCalulcator.CalculateHeatIndex(temperature, humidity);
            Console.WriteLine($"Updating measured temp - old: {savedTemperature.MeasuredTemperature.Value}, new: {temperature}");

            if (savedTemperature.MeasuredTemperature.Value != temperature)
            {
                savedTemperature.MeasuredTemperature.Value = temperature;

                await UpdateTermostatState(savedTemperature).ConfigureAwait(false);
                await UpdateFixtureRegister(savedTemperature).ConfigureAwait(false);
            }
        }

        private Task UpdateFixtureRegister(Temperature fixture)
             => hubClient.SendAsync(new[] { fixture.MeasuredTemperature }, EventBus.Messaging.MethodType.Update, "FixtureRegister");

        private Task MeasureAllTemperatures()
        {
            var tasks = temperatureSensorBaseUrls.Keys.Select(k => MeasureTemperature(k)).ToList();
            return Task.WhenAll(tasks);
        }

        private async Task MeasureTemperature(string name)
        {
            try
            {
                await httpClient.GetAsync(GetTemperatureSensorUpdateUrl(name)).ConfigureAwait(false);
                var result = await httpClient.GetAsync(GetTemperatureSensorMeasureUrl(name)).ConfigureAwait(false);
                var measured = await result.Content.ReadAsAsync<TemperatureMeasureResult>().ConfigureAwait(false);
                await UpdateMeasuredTemperature(name, measured.Variables.RealTemperature, measured.Variables.RealHumidity).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to measure temperature {name}. {e}");
            }
        }
        private static string GetTemperatureSensorMeasureUrl(string name)
            => $"{temperatureSensorBaseUrls[name]}";
        private static string GetTemperatureSensorUpdateUrl(string name)
            => GetTemperatureSensorMeasureUrl(name) + "update";

        Task UpdateTermostatState(Temperature fixture)
        {
            if (fixture.MeasuredTemperature.Value < fixture.TargetTemperature.Value)
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
                try
                {
                    await httpClient.GetAsync(url).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
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
