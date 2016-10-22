using Plugins.Common.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperaturePlugin
{
    static class TemperatureManager
    {
        static IDictionary<string, Plugins.Common.Fixtures.Temperature> temperatures = new Dictionary<string, Plugins.Common.Fixtures.Temperature>();
        static IDictionary<string, bool> termostatStatuses = new Dictionary<string, bool>();

        internal static Task Update(string name, double temperature, double humidity)
        {
            Plugins.Common.Fixtures.Temperature fixture;
            if (!temperatures.TryGetValue(name, out fixture))
            {
                fixture = new Plugins.Common.Fixtures.Temperature { Name = "Temperatur", MeasuredTemperature = new Channel { Name = name }, TargetTemperature = new Channel() };
                temperatures[name] = fixture;
            }

            // no perceived temperature for now as the heat index works only above 26°C
            //var perceivedTemperature = HeatIndexCalulcator.CalculateHeatIndex(temperature, humidity);
            fixture.MeasuredTemperature.Value = temperature;
            Console.WriteLine($"{fixture.Name}: {fixture.MeasuredTemperature.Value:N2} (Target: {fixture.TargetTemperature.Value:N2})");

            return UpdateFixtureRegister(fixture);
        }

        private static Task UpdateFixtureRegister(Plugins.Common.Fixtures.Temperature fixture)
             => TemperatureHubClient.Instance.SendAsync(new[] { fixture.MeasuredTemperature }, EventBus.Messaging.MethodType.Update, "FixtureRegister");

        static void UpdateTermostatState(Plugins.Common.Fixtures.Temperature fixture)
        {
            if (fixture.MeasuredTemperature.Value < fixture.TargetTemperature.Value)
                TryOpenTermostat(fixture.Name);
            else
                TryCloseTermostat(fixture.Name);
        }

        private static void TryOpenTermostat(string name)
        {
            var isOpen = false;
            termostatStatuses.TryGetValue(name, out isOpen);
            if (!isOpen)
            {
                // send open command
                termostatStatuses[name] = true;
            }
        }

        private static void TryCloseTermostat(string name)
        {
            var isOpen = false;
            termostatStatuses.TryGetValue(name, out isOpen);
            if (isOpen)
            {
                // send close command
                termostatStatuses[name] = false;
            }
        }
    }
}
