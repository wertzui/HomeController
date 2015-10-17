using Plugins.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginHost
{
    /// <summary>
    /// This class is used to unload all classes implementing IPlugin in an Assembly file across AppDomains.
    /// </summary>
    [Serializable]
    public class PluginUnloader
    {
        /// <summary>
        /// Unloads all plugins that.
        /// </summary>
        public void UnloadPlugins()
        {
            foreach (var plugin in PluginRegister.Plugins)
            {
                Console.WriteLine("Stopping plugin {0}", plugin.GetType().Name);
                var noTimeout = plugin.StopAsync().Wait(30000);
                if (noTimeout)
                    Console.WriteLine("Plugin {0} stopped", plugin.GetType().Name);
                else
                    Console.WriteLine("Plugin {0} timed out", plugin.GetType().Name);
            }
        }
    }
}
