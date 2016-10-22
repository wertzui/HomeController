using EventBus.Messaging;
using Plugins.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Plugins.Common.Fixtures;
using Microsoft.Owin.Hosting;

namespace TemperaturePlugin
{
    /// <summary>
    /// Hub client for the fixture register that handles the loading and update of all fixtures.
    /// </summary>
    public class TemperatureHubClient : HubClientBase
    {
        const string url = "http://*:1907";
        IDisposable webApp;
        internal static TemperatureHubClient Instance { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemperatureHubClient"/> class.
        /// </summary>
        public TemperatureHubClient()
            : base(nameof(TemperaturePlugin), nameof(TemperaturePlugin))
        {
            Instance = this;
        }

        protected override Task OnBeforeConnectionStartAsync()
        {
            webApp = WebApp.Start<Startup>(url);
            Console.WriteLine($"Temperature Server running at {url}");

            return base.OnBeforeConnectionStartAsync();
        }

        /// <summary>
        /// Called when a message is received.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected override async Task OnReceiveAsync(Message message)
        {
            switch (message.Method)
            {
                case MethodType.Update:
                    UpdateChannels(message.Values);
                    break;
            }
        }

        private void UpdateChannels(IEnumerable<dynamic> values)
        {
            foreach (var value in values)
            {
                var room = value as Room;
                if(room != null)
                {

                }
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                webApp.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}