namespace TemperaturePlugin
{
    public class TemperatureMeasureResult
    {
        public Variables Variables { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Hardware { get; set; }
        public bool Connected { get; set; }
    }

    public class Variables
    {
        public int Temperature { get; set; }
        public int Humidity { get; set; }

        public double RealTemperature => Temperature / 10.0;
        public double RealHumidity => Humidity / 10.0;
    }
}