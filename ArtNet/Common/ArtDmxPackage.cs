using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtNet.Common
{
    /// <summary>
    /// An ArtDmx package that contains dmx values encapsulated in an ArtNet package.
    /// </summary>
    public class ArtDmxPackage : IArtNetPackage
    {
        #region Easy Access

        static readonly char[] ID = { 'A', 'r', 't', '-', 'N', 'e', 't', '\0' };
        const OpCodes OpCode = OpCodes.OpDmx;
        const short ProtVer = 14;


        const byte Physical = 0;

        const byte HeaderLength = 18;
        const short MinDataLength = 2;
        const short MaxDataLength = 512;
        public const short MinAddress = 1;
        public const short MaxAddress = 512;

        /// <summary>
        /// Gets or sets the ArtNet sequence number.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public byte Sequence { get; set; }
        /// <summary>
        /// Gets or sets the ArtNet universe.
        /// </summary>
        /// <value>
        /// The universe.
        /// </value>
        public short Universe { get; set; }

        /// <summary>
        /// Gets or sets the length of the data.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public short Length => (short)Data.Length;

        #endregion Easy Access

        #region Everything converted to bytes

        static readonly byte[] bID = ID.Select(c => (byte)c).ToArray();
        static readonly byte OpCodeHi = ByteHelper.HiByte((short)OpCode);
        static readonly byte OpCodeLo = ByteHelper.LoByte((short)OpCode);
        static readonly byte ProtVerHi = ByteHelper.HiByte((short)ProtVer);
        static readonly byte ProtVerLo = ByteHelper.LoByte((short)ProtVer);

        byte SubUni => ByteHelper.LoByte(Universe);
        byte Net => ByteHelper.HiByte(Universe);
        byte LengthHi => ByteHelper.HiByte(Length);
        byte LengthLo => ByteHelper.LoByte(Length);

        #endregion Everything converted to bytes

        /// <summary>
        /// Gets the raw data array.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public byte[] Data { get; } = new byte[MaxDataLength];

        /// <summary>
        /// Constructs the ArtNet header.
        /// </summary>
        /// <returns></returns>
        byte[] ConstructHeader()
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

        /// <summary>
        /// Gets the bytes for the full ArtDmx package that encapsulates the raw data array.
        /// This can be send to the ArtNet using a Socket.
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            var header = ConstructHeader();

            var result = new byte[header.Length + Data.Length];

            Buffer.BlockCopy(header, 0, result, 0, HeaderLength);
            Buffer.BlockCopy(Data, 0, result, HeaderLength, Data.Length);

            return result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtDmxPackage" /> class.
        /// </summary>
        public ArtDmxPackage()
        {
            Sequence = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtDmxPackage" /> class.
        /// </summary>
        /// <param name="data">The initial data.</param>
        public ArtDmxPackage(byte[] data)
            : this()
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var min = Math.Min(MaxDataLength, data.Length);

            Buffer.BlockCopy(data, 0, this.Data, 0, min);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtDmxPackage" /> class.
        /// </summary>
        /// <param name="addressAndValuePairs">The address and value pairs with initial data.</param>
        public ArtDmxPackage(params AddressValuePair[] addressAndValuePairs)
            : this()
        {
            if (addressAndValuePairs == null)
                throw new ArgumentNullException(nameof(addressAndValuePairs));

            Set(addressAndValuePairs);
        }

        /// <summary>
        /// Sets the specified address value pairs.
        /// </summary>
        /// <param name="pairs">The pairs.</param>
        public void Set(params AddressValuePair[] pairs)
        {
            Set((IEnumerable<AddressValuePair>)pairs);
        }

        /// <summary>
        /// Sets the specified address value pairs.
        /// </summary>
        /// <param name="pairs">The pairs.</param>
        public void Set(IEnumerable<AddressValuePair> pairs)
        {
            foreach (var pair in pairs)
            {
                Set(pair);
            }
        }

        /// <summary>
        /// Sets the specified address value pair.
        /// </summary>
        /// <param name="pair">The pair.</param>
        public void Set(AddressValuePair pair)
        {
            Set(pair.Address, pair.Value);
        }

        /// <summary>
        /// Sets the specified DMX address to the specified value.
        /// </summary>
        /// <param name="address">The DMX address.</param>
        /// <param name="value">The value.</param>
        public void Set(short address, byte value)
        {
            if (address >= MinAddress && address <= MaxAddress)
                Data[address - 1] = value;
        }

        /// <summary>
        /// Gets the value for the specified DMX address.
        /// </summary>
        /// <param name="address">The DMX address.</param>
        public byte Get(short address) => Data[address - 1];
    }
}