using System;
using System.Threading.Tasks;

namespace Plugins.Common
{
    /// <summary>
    /// Interface for all Plugins only plugins implementing this interface will be loaded by the plugin host.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Starts the plugin.
        /// </summary>
        /// <param name="hubUri">The hub URI.</param>
        /// <returns></returns>
        Task StartAsync(string hubUri);

        /// <summary>
        /// Stops the plugin.
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
    }
}