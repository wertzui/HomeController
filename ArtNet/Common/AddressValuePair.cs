namespace ArtNet.Common
{
    /// <summary>
    /// An DMX adress with a value.
    /// </summary>
    public struct AddressValuePair
    {
        /// <summary>
        /// Gets the DMX address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public short Address { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public byte Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressValuePair"/> struct.
        /// </summary>
        /// <param name="address">The DMX address.</param>
        /// <param name="value">The value.</param>
        public AddressValuePair(short address, byte value)
        {
            Address = address;
            Value = value;
        }
    }
}