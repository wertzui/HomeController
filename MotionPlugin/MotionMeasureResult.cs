namespace MotionPlugin
{

    public class MotionMeasureResult
    {
        public int Return_value { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Hardware { get; set; }
        public bool Connected { get; set; }

        public bool MotionDetected => Return_value > 0;
    }

}