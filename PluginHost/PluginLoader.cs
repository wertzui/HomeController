using Plugins.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PluginHost
{
    /// <summary>
    /// This class is used to load all classes implementing IPlugin in an Assembly file across AppDomains.
    /// </summary>
    [Serializable]
    public class PluginLoader// : MarshalByRefObject
    {
        /// <summary>
        /// Gets or sets the file that contains the assembly that will be searched for plugins.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public string File { get; set; }

        /// <summary>
        /// Gets or sets the URI to the event hub from the EventBus project.
        /// </summary>
        /// <value>
        /// The hub URI.
        /// </value>
        public string HubUri { get; set; }

        /// <summary>
        /// Stores the number of loaded plugins after <see cref="LoadPluginsAsync"/> was called.
        /// </summary>
        /// <value>
        /// The number of loaded plugins.
        /// </value>
        public int NumberOfLoadedPlugins { get; set; }

        /// <summary>
        /// Loads all plugins in the given assenmbly.
        /// </summary>
        public async Task LoadPluginsAsync()
        {
            var plugins = new List<IPlugin>();

            Console.WriteLine("Loading Assembly");
            var pluginAssembly = Assembly.LoadFile(File);
            Console.WriteLine("Assembly loaded");
            var pluginTypes = pluginAssembly.ExportedTypes.Where(t => t.GetInterfaces().Any(i => i.Equals(typeof(IPlugin))));
            Console.WriteLine("Found {0} types implementing IPlugin", pluginTypes.Count());
            foreach (var pluginType in pluginTypes)
            {
                try
                {
                    Console.WriteLine("Looking for a parameterless Constructor in {0}", pluginType.Name);
                    var constructor = pluginType.GetConstructor(Type.EmptyTypes);
                    if (constructor != null)
                    {
                        Console.WriteLine("Found a parameterless constructor");
                        var plugin = (IPlugin)constructor.Invoke(null);
                        plugins.Add(plugin);
                        Console.WriteLine("Plugin of type {0} created", pluginType.Name);
                        await plugin.StartAsync(HubUri).ConfigureAwait(false);
                        Console.WriteLine("{0}.StartAsync({1}) called", pluginType.Name, HubUri);
                    }
                    else
                        Console.WriteLine("No Constructor found");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception occured");
                    Console.WriteLine(e);
                }
            }

            PluginRegister.Plugins.AddRange(plugins);
            NumberOfLoadedPlugins = plugins.Count;
        }
    }
}