using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginHost
{
    /// <summary>
    /// This will host all plugins inside a console window.
    /// Each plugin is loaded into a seperate <see cref="AppDomain"/> so they cannot influence each other.
    /// </summary>
    class Program
    {
        static IDictionary<string, AppDomain> appDomains = new Dictionary<string, AppDomain>();
        const string hubUri = "http://localhost:1906/";

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            var pluginPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) + "\\Plugins";
            var files = Directory.GetFiles(pluginPath, "*Plugin.dll");
            Console.WriteLine("Found {0} plugin files", files.Count());
            foreach (var file in files)
            {
                Console.WriteLine("Loading {0}", file);
                Console.WriteLine("Creating AppDomain");
                var setup = new AppDomainSetup
                {
                    PrivateBinPath = pluginPath
                };
                var domain = AppDomain.CreateDomain(file, null, setup);

                var loader = new PluginLoader { File = file, HubUri = hubUri };
                domain.DoCallBack(new CrossAppDomainDelegate(loader.LoadPlugins));

                //if (loader.NumberOfLoadedPlugins > 0)
                //{
                //    Console.WriteLine("Loaded {0} plugins from {1}", loader.NumberOfLoadedPlugins, file);
                //    appDomains.Add(file, domain);
                //}
                //else
                //{
                //    Console.WriteLine("{0} did not contain any plugins, unloading AppDomain", file);
                //    AppDomain.Unload(domain);
                //    Console.WriteLine("Appdomain Unloaded for file {0}", file);
                //}
                appDomains.Add(file, domain);
            }
            Console.WriteLine("All plugins loaded. Press any key to stop");

            Console.ReadKey();

            Console.WriteLine("Unloading Plugins");
            foreach (var domainAndPlugins in appDomains)
            {
                Console.WriteLine("Unloading plugins in {0}", domainAndPlugins.Key);

                var unloader = new PluginUnloader();
                domainAndPlugins.Value.DoCallBack(new CrossAppDomainDelegate(unloader.UnloadPlugins));

                AppDomain.Unload(domainAndPlugins.Value);
                Console.WriteLine("All plugins in {0} unloaded", domainAndPlugins.Key);
            }

            Console.WriteLine("All plugins unloaded. Press any key to exit");
            Console.ReadKey();
        }





    }
}
