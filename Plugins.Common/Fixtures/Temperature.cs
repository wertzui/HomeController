using System.Collections.Generic;

namespace Plugins.Common.Fixtures
{
    /// <summary>
    /// A Temperature Sensor and maybe a Target temperature for that Room
    /// </summary>
    /// <seealso cref="Plugins.Common.Fixtures.FixtureBase" />
    public class Temperature : FixtureBase
    {
        /// <summary>
        /// Gets or sets the measured temperature in degrees celsius.
        /// </summary>
        /// <value>
        /// The measured temperature.
        /// </value>
        public Channel MeasuredTemperature { get; set; }

        /// <summary>
        /// Gets or sets the measured humidity in percent.
        /// </summary>
        /// <value>
        /// The measured temperature.
        /// </value>
        public Channel MeasuredHumidity { get; set; }

        /// <summary>
        /// Gets or sets the target temperature in degrees celsius.
        /// </summary>
        /// <value>
        /// The target temperature.
        /// </value>
        public Channel TargetTemperature { get; set; }

        /// <summary>
        /// used internally to iterate through all channels.
        /// </summary>
        /// <value>
        /// The channels.
        /// </value>
        private IEnumerable<Channel> channels
            => new[] { MeasuredTemperature, MeasuredHumidity, TargetTemperature };

        /// <summary>
        /// Updates the channels for this light with the new values.
        /// </summary>
        /// <param name="channels">The new channel values.</param>
        public void UpdateChannels(IEnumerable<Channel> channels)
        {
            foreach (var channel in this.channels)
            {
                foreach (var newChannel in channels)
                {
                    if (channel.Name == newChannel.Name)
                    {
                        channel.Value = newChannel.Value;
                    }
                }
            }
        }
    }
}