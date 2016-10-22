namespace TemperaturePlugin
{
    internal class HeatIndexCalulcator
    {
        // constants needed to calculate the heat index
        private const double C1 = -8.784695;
        private const double C2 = 1.61139411;
        private const double C3 = 2.338549;
        private const double C4 = -0.14611605;
        private const double C5 = -0.012308094;
        private const double C6 = -0.016424828;
        private const double C7 = 0.002211732;
        private const double C8 = 0.00072546;
        private const double C9 = -0.000003582;

        public static double CalculateHeatIndex(double temperature, double humidity)
            => C1 + C2 * temperature + C3 * humidity + C4 * temperature * humidity + C5 * temperature * temperature + C6 * humidity * humidity + C7 * temperature * temperature * humidity + C8 * temperature * humidity * humidity + C9 * temperature * temperature * humidity * humidity;
    }
}