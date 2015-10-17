using System.Threading.Tasks;

namespace Plugins.Common
{
    /// <summary>
    /// Base class for plugins implementing common plugin behavior.
    /// </summary>
    public abstract class PluginBase<THubClient> : IPlugin
        where THubClient : HubClientBase, new()
    {
        private THubClient hub;

        /// <summary>
        /// Starts the plugin.
        /// </summary>
        /// <param name="hubUri">The hub URI.</param>
        /// <returns></returns>
        public async Task StartAsync(string hubUri)
        {
            hub = new THubClient
            {
                Url = hubUri // must be done this way as the constructor must be parameterless
            };
            await hub.StartAsync();
        }

        /// <summary>
        /// Stops the plugin.
        /// </summary>
        /// <returns></returns>
        public async Task StopAsync()
        {
            Dispose();
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    hub.Dispose();
                }

                hub = null;

                disposedValue = true;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}