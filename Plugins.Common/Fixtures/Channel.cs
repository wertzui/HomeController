namespace Plugins.Common.Fixtures
{
    /// <summary>
    /// Represents one channel of a fixture.
    /// For a light this may be an intensity channel,
    /// for a heat sensor this might be degrees.
    /// </summary>
    public class Channel : FixtureBase
    {
        /// <summary>
        /// Gets or sets the type of the channel.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the target that will receive updates if the channel value is changed in the frontend.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public string Target { get; set; }
        /// <summary>
        /// Gets or sets the minimum value of this channel.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public double Min { get; set; }
        /// <summary>
        /// Gets or sets the maximum value of this channel.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public double Max { get; set; }
        /// <summary>
        /// Gets or sets the current value of this channel.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value { get; set; }
    }
}