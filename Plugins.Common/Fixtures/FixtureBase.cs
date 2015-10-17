using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Plugins.Common.Fixtures
{
    /// <summary>
    /// Base class for all Fixtures.
    /// Everything that is in the real world is represented as fixture from room to smoke sensor.
    /// </summary>
    public abstract class FixtureBase
    {
        /// <summary>
        /// Gets or sets the name of this fixture.
        /// The name must be globally unique.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty(Order = -2)] // for easier reading the Name should appear before other values
        public string Name { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}