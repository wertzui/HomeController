using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.Common.Fixtures
{
    /// <summary>
    /// Represents a room in a house
    /// </summary>
    public class Room : FixtureBase
    {
        /// <summary>
        /// Gets or sets the lights inside this room.
        /// </summary>
        /// <value>
        /// The lights.
        /// </value>
        public IEnumerable<Light> Lights { get; set; }

        /// <summary>
        /// Updates the channels for all fixtures inside this room with the new values.
        /// </summary>
        /// <param name="channels">The new channel values.</param>
        public void UpdateChannels(IEnumerable<Channel> channels)
        {
            foreach (var light in Lights)
            {
                light.UpdateChannels(channels);
            }
        }
    }
}
