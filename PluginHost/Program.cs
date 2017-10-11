using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PluginHost
{
    /// <summary>
    /// This will host all plugins inside a console window.
    /// Each plugin is loaded into a seperate <see cref="AppDomain"/> so they cannot influence each other.
    /// </summary>
    internal class Program
    {
        private const string hubUri = "http://localhost:1906/";

        /// <summary>
        /// Main entry point
        /// </summary>
        private static void Main()
        {
            var pluginPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) + "\\Plugins";
            var files = Directory.GetFiles(pluginPath, "*Plugin.dll");
            Console.WriteLine("Found {0} plugin files in folder {1}", files.Count(), pluginPath);
            foreach (var file in files)
            {
                Console.WriteLine("Loading {0}", file);

                var loader = new PluginLoader { File = file, HubUri = hubUri };
                loader.LoadPluginsAsync().GetAwaiter().GetResult();
            }
            Console.WriteLine("All plugins loaded. Press any key to stop");

            Console.Read();

            Console.WriteLine("Unloading Plugins");
            var unloader = new PluginUnloader();
            unloader.UnloadPlugins();

            Console.WriteLine("All plugins unloaded. Press any key to exit");
            Console.ReadKey();
        }
    }
}