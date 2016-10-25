using Plugins.Common.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TemperaturePlugin
{
    static class TemperatureManager
    {
        // key is temperature fixture name, not room name
        static IDictionary<string, Temperature> temperatures = new Dictionary<string, Temperature>();
        static IDictionary<string, bool> termostatStatuses = new Dictionary<string, bool>();
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
        const string baseUrl = "http://192.168.0.55/digital/";
        static int targetStringLength = "TargetTemperature".Length;
        static int measuredStringLength = "MeasuredTemperature".Length;

        internal static void UpdateTargetTemperatures(IEnumerable<Channel> channels)
        {
            foreach (var channel in channels)
            {
                UpdateTargetTemperature(channel);
            }
        }

        private static void UpdateTargetTemperature(Channel channel)
        {
            var temperatureName = GetFixtureNameFromTargetChannel(channel.Name);
            var savedTemperature = GetOrAddTemperature(temperatureName);

            Console.WriteLine($"Updating temp - old: {savedTemperature.TargetTemperature.Value}, new: {channel.Value}");
            savedTemperature.TargetTemperature.Value = channel.Value;

            UpdateTermostatState(savedTemperature);
        }

        private static string GetFixtureNameFromTargetChannel(string channelName) => channelName.Substring(targetStringLength);


        private static Temperature GetOrAddTemperature(string temperatureName)
        {
            Temperature temperature;
            if(!temperatures.TryGetValue(temperatureName, out temperature))
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

        //internal static Task UpdateMeasured(string name, double temperature, double humidity)
        //{
        //    Plugins.Common.Fixtures.Temperature fixture;
        //    if (!temperatures.TryGetValue(name, out fixture))
        //    {
        //        fixture = new Plugins.Common.Fixtures.Temperature { Name = "Temperatur", MeasuredTemperature = new Channel { Name = name }, TargetTemperature = new Channel() };
        //        temperatures[name] = fixture;
        //    }

        //    // no perceived temperature for now as the heat index works only above 26°C
        //    //var perceivedTemperature = HeatIndexCalulcator.CalculateHeatIndex(temperature, humidity);
        //    fixture.MeasuredTemperature.Value = temperature;
        //    Console.WriteLine($"{fixture.Name}: {fixture.MeasuredTemperature.Value:N2} (Target: {fixture.TargetTemperature.Value:N2})");

        //    return UpdateFixtureRegister(fixture);
        //}

        //private static Task UpdateFixtureRegister(Plugins.Common.Fixtures.Temperature fixture)
        //     => TemperatureHubClient.Instance.SendAsync(new[] { fixture.MeasuredTemperature }, EventBus.Messaging.MethodType.Update, "FixtureRegister");

        static Task UpdateTermostatState(Temperature fixture)
        {
            if (fixture.MeasuredTemperature.Value < fixture.TargetTemperature.Value)
                return TryOpenTermostat(fixture.Name);
            else
                return TryCloseTermostat(fixture.Name);
        }

        private static async Task TryOpenTermostat(string name)
        {
            Console.WriteLine($"Trying to open termonstat {name}");
            var isOpen = false;
            termostatStatuses.TryGetValue(name, out isOpen);
            Console.WriteLine($"Current status is {isOpen}");
            if (!isOpen)
            {
                // send open command
                var url = GetUrl(name, true);
                Console.WriteLine($"Opening termostat with URL {url}");
                using (var client = new HttpClient())
                {
                    await client.GetAsync(url);
                }
                termostatStatuses[name] = true;
            }
        }

        private static async Task TryCloseTermostat(string name)
        {
            Console.WriteLine($"Trying to close termonstat {name}");
            var isOpen = false;
            termostatStatuses.TryGetValue(name, out isOpen);
            Console.WriteLine($"Current status is {isOpen}");
            if (isOpen)
            {
                // send close command
                var url = GetUrl(name, false);
                Console.WriteLine($"Closing termostat with URL {url}");
                using (var client = new HttpClient())
                {
                    await client.GetAsync(url);
                }
                termostatStatuses[name] = false;
            }
        }

        private static string GetUrl(string name, bool targetTermostatStatus)
            => $"{baseUrl}{RelaisNumbers[name]}/{(targetTermostatStatus ? 0 : 1)}";
    }
}
