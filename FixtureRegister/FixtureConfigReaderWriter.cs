using Newtonsoft.Json;
using Plugins.Common.Fixtures;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace FixtureRegister
{
    /// <summary>
    /// Allows easy access to the rooms.json file
    /// </summary>
    internal class FixtureConfigReaderWriter
    {
        /// <summary>
        /// Gets or sets the configuration file path (rooms.json).
        /// </summary>
        /// <value>
        /// The configuration file path.
        /// </value>
        internal string configFilePath { get; set; } = Path.GetDirectoryName((Assembly.GetExecutingAssembly().Location)) + "\\rooms.json";

        /// <summary>
        /// Reads the rooms from the configuration asynchronous.
        /// </summary>
        /// <returns></returns>
        internal async Task<IEnumerable<Room>> ReadConfigAsync()
        {
            string fileContent;
            using (var reader = File.OpenText(configFilePath))
            {
                fileContent = await reader.ReadToEndAsync();
            }
            return JsonConvert.DeserializeObject<List<Room>>(fileContent);
        }

        /// <summary>
        /// Writes the rooms to the configuration asynchronous.
        /// </summary>
        /// <param name="rooms">The rooms.</param>
        /// <returns></returns>
        internal async Task WriteConfigAsync(IEnumerable<Room> rooms)
        {
            var fileContent = JsonConvert.SerializeObject(rooms, Formatting.Indented);
            using (var writer = new StreamWriter(configFilePath))
            {
                await writer.WriteAsync(fileContent);
            }
        }
    }
}