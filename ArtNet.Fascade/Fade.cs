using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtNet.Fascade
{
    class Fade
    {
        private short address;
        private byte value;
        private byte originalValue;
        private double milliSeconds;

        public Fade(short address, byte value, byte originalValue, double milliSeconds)
        {
            // TODO: Complete member initialization
            this.address = address;
            this.value = value;
            this.originalValue = originalValue;
            this.milliSeconds = milliSeconds;
        }
    }
}
