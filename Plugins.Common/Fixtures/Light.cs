using System.Collections.Generic;

namespace Plugins.Common.Fixtures
{
    /// <summary>
    /// Represents a light
    /// </summary>
    public class Light : FixtureBase
    {
        /// <summary>
        /// Gets or sets the channels for this light.
        /// A lamp normally has a white channel, but may contain more channels, such as RGB.
        /// </summary>
        /// <value>
        /// The channels.
        /// </value>
        public IEnumerable<Channel> Channels { get; set; }

        /// <summary>
        /// Updates the channels for this light with the new values.
        /// </summary>
        /// <param name="channels">The new channel values.</param>
        public void UpdateChannels(IEnumerable<Channel> channels)
        {
            foreach (var channel in Channels)
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