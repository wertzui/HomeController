using ArtNet.Fascade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Fixtures
{
    public abstract class ArtNetFixture : IArtNetFixture
    {
        public short StartAddress { get; private set; }

        protected readonly Timer fadeTimer;

        protected readonly ArtNetFascade fascade;

        protected byte DoubleToByte(double intensity)
        {
            return (byte)(intensity * 255);
        }
        protected double ByteToDouble(byte intensity)
        {
            return intensity / 255.0;
        }

        public ArtNetFixture(ArtNetFascade fascade, short startAddress)
        {
            if (fascade == null)
                throw new ArgumentNullException("fascade");
            if (startAddress < 1 || startAddress > 512)
                throw new ArgumentOutOfRangeException("startAddress", "startAddress must be between 1 and 512");

            fadeTimer = new Timer(10);
            fadeTimer.AutoReset = true;
            this.fascade = fascade;
            StartAddress = startAddress;
        }
    }
}
