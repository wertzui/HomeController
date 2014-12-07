using System;
using System.Linq;

namespace ArtNet.Common
{
    public class ArtDmxPackage : IArtNetPackage
    {
        #region Easy Access

        public static readonly char[] ID = new char[] { 'A', 'r', 't', '-', 'N', 'e', 't', '\0' };
        public const OpCodes OpCode = OpCodes.OpDmx;
        public const short ProtVer = 14;

        public byte Sequence { get; set; }

        public const byte Physical = 0;
        public short Universe = 0;

        public short Length { get { return (short)data.Length; } }

        public const byte HeaderLength = 18;
        public const short MinDataLength = 2;
        public const short MaxDataLength = 512;
        public const short MinAddress = 1;
        public const short MaxAddress = 512;

        #endregion Easy Access

        #region Everything converted to bytes

        private static readonly byte[] bID = ID.Select(c => (byte)c).ToArray();
        private static readonly byte OpCodeHi = ByteHelper.HiByte((short)OpCode);
        private static readonly byte OpCodeLo = ByteHelper.LoByte((short)OpCode);
        private static readonly byte ProtVerHi = ByteHelper.HiByte((short)ProtVer);
        private static readonly byte ProtVerLo = ByteHelper.LoByte((short)ProtVer);

        private byte SubUni { get { return ByteHelper.LoByte(Universe); } }

        private byte Net { get { return ByteHelper.HiByte(Universe); } }

        private byte LengthHi { get { return ByteHelper.HiByte(Length); } }

        private byte LengthLo { get { return ByteHelper.LoByte(Length); } }

        #endregion Everything converted to bytes

        public readonly byte[] data = new byte[MaxDataLength];

        private byte[] ConstructHeader()
        {
            var header = new byte[HeaderLength];
            Array.Copy(bID, header, 8);
            header[8] = OpCodeLo;
            header[9] = OpCodeHi;
            header[10] = ProtVerHi;
            header[11] = ProtVerLo;
            header[12] = Sequence;
            header[13] = Physical;
            header[14] = SubUni;
            header[15] = Net;
            header[16] = LengthHi;
            header[17] = LengthLo;

            Sequence = (byte)(Sequence % 127 + 1);

            return header;
        }

        public byte[] GetBytes()
        {
            var header = ConstructHeader();

            var result = new byte[header.Length + data.Length];

            Buffer.BlockCopy(header, 0, result, 0, HeaderLength);
            Buffer.BlockCopy(data, 0, result, HeaderLength, data.Length);

            return result;
        }

        public ArtDmxPackage()
        {
            Sequence = 1;
        }

        public ArtDmxPackage(byte[] data)
            : this()
        {
            if (data == null)
                throw new ArgumentNullException("data");

            var min = Math.Min(MaxDataLength, data.Length);

            Buffer.BlockCopy(data, 0, this.data, 0, min);
        }

        public ArtDmxPackage(params AddressValuePair[] addressAndValuePairs)
            : this()
        {
            if (addressAndValuePairs == null)
                throw new ArgumentNullException("addressAndValuePairs");

            Set(addressAndValuePairs);
        }

        public void Set(params AddressValuePair[] pairs)
        {
            foreach (var pair in pairs)
            {
                Set(pair);
            }
        }

        public void Set(AddressValuePair pair)
        {
            Set(pair.Address, pair.Value);
        }

        public void Set(short address, byte value)
        {
            if (address >= MinAddress && address <= MaxAddress)
                data[address - 1] = value;
        }

        public ArtDmxPackage(short address1, byte value1)
            : this(new AddressValuePair(address1, value1))
        { }

        public ArtDmxPackage(short address1, byte value1, short address2, byte value2)
            : this(new AddressValuePair(address1, value1),
                new AddressValuePair(address2, value2))
        { }

        public ArtDmxPackage(short address1, byte value1, short address2, byte value2, short address3, byte value3)
            : this(new AddressValuePair(address1, value1),
                new AddressValuePair(address2, value2),
                new AddressValuePair(address3, value3))
        { }

        public ArtDmxPackage(short address1, byte value1, short address2, byte value2, short address3, byte value3, short address4, byte value4)
            : this(new AddressValuePair(address1, value1),
                new AddressValuePair(address2, value2),
                new AddressValuePair(address3, value3),
                new AddressValuePair(address4, value4))
        { }
    }
}