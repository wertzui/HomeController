using ArtNet.Fascade;
using System;

namespace Fixtures
{
    public class ArtNetWhiteFixture : ArtNetFixture, IArtNetWhiteFixture
    {
        public string Name { get; private set; }

        public double Intensity
        {
            get { return intensity; }
            set
            {
                fadeTimer.Stop();
                intensity = value;
                fascade.Set(StartAddress, DoubleToByte(value));
            }
        }

        private double intensity;
        private double fadeTargetIntensity;
        private double fadeStartIntensity;
        private double fadeTime;
        private DateTime fadeStartTime;

        public ArtNetWhiteFixture(ArtNetFascade fascade, short startAddress, string name)
            : base(fascade, startAddress)
        {
            fadeTimer.Elapsed += fadeTimer_Elapsed;
            Name = name;
            Intensity = ByteToDouble(fascade.Get(startAddress));
        }

        public void Fade(double intensity, double milliseconds)
        {
            fadeTargetIntensity = intensity;
            fadeStartIntensity = Intensity;
            fadeTime = milliseconds;
            fadeStartTime = DateTime.Now;
            Console.WriteLine("start: " + fadeStartIntensity + " target: " + fadeTargetIntensity + " time: " + fadeTime);

            if (!fadeTimer.Enabled)
                fadeTimer.Start();

        }

        void fadeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var elapsedMilliseconds = (DateTime.Now - fadeStartTime).TotalMilliseconds;
            if (elapsedMilliseconds < fadeTime)
            {
                intensity = fadeStartIntensity + (fadeTargetIntensity - fadeStartIntensity) * (elapsedMilliseconds / fadeTime);
                var bytes = DoubleToByte(intensity);
                Console.WriteLine(elapsedMilliseconds.ToString("N0") + " - " + intensity + " - " + bytes);
                fascade.Set(StartAddress, bytes);
            }
            else
            {
                Intensity = fadeTargetIntensity;
            }
        }
    }
}