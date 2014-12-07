using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtNet.Common
{
    public struct AddressValuePair
    {
        public readonly short Address;
        public readonly byte Value;
        public AddressValuePair(short address, byte value)
        {
            Address = address;
            Value = value;
        }
    }
}
