using System.Collections.Generic;

namespace ArtNet.Common
{
    /// <summary>
    /// Class to help with bytes when dealing with ArtDmx packages.
    /// </summary>
    public static class ByteHelper
    {
        /// <summary>
        /// Gets the value as low byte.
        /// </summary>
        /// <param name="wParam">The w parameter.</param>
        /// <returns></returns>
        public static byte LoByte(short wParam)
        {
            return (byte)(wParam & 255);
        }

        /// <summary>
        /// Gets the value as high byte.
        /// </summary>
        /// <param name="wParam">The w parameter.</param>
        /// <returns></returns>
        public static byte HiByte(short wParam)
        {
            return (byte)((wParam / 256) & 255);
        }

        /// <summary>
        /// Gets the addresses and values from and ArtDmx message.
        /// </summary>
        /// <param name="artNetMessage">The art net message.</param>
        /// <returns></returns>
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