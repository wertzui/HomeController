using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtNet.Common
{
    public static class ByteHelper
    {
        public static byte LoByte(short wParam)
        {
            return (byte)(wParam & 255);
        }

        public static byte HiByte(short wParam)
        {
            return (byte)((wParam / 256) & 255);
        }

        public static IEnumerable<AddressValuePair> GetAddressesAndValuesFromMessage(byte[] artNetMessage)
        {
            var pairs = new List<AddressValuePair>();

            for (int i = 18; i < artNetMessage.Length; i++)
            {
                var value = artNetMessage[i];
                if (value != 0)
                {
                    var address = (short)(i - 17);
                    var pair = new AddressValuePair(address, value);
                    pairs.Add(pair);
                }
            }

            return pairs;
        }
    }
}
