using EventBus.Messaging;
using Plugins.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Plugins.Common.Fixtures;
using Microsoft.Owin.Hosting;
using System.Linq;

namespace TemperaturePlugin
{
    /// <summary>
    /// Hub client for the fixture register that handles the loading and update of all fixtures.
    /// </summary>
    public class TemperatureHubClient : HubClientBase
    {
        private readonly TemperatureManager temperatureManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemperatureHubClient"/> class.
        /// </summary>
        public TemperatureHubClient()
            : base("Temperature", "Temperature")
        {
            temperatureManager = new TemperatureManager(this);
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
                    await UpdateReceived(message).ConfigureAwait(false);
                    break;
            }
        }

        async Task UpdateReceived(Message message)
        {
            if (message.Values != null)
            {
                var channels = GetChannelsFromMessage(message.Values);

                // send values to art net
                await temperatureManager.UpdateTargetTemperatures(channels).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Extracts key value pairs from the message values.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        static IEnumerable<Channel> GetChannelsFromMessage(IEnumerable<dynamic> channels)
        {
            var realChannels = new List<Channel>();
            if (channels != null)
            {
                foreach (var channel in channels)
                {
                    var name = channel.Name;
                    var value = channel.Value;

                    if (name != null && value != null)
                    {
                        var realName = name.ToString();
                        double realValue;
                        if (double.TryParse(value.ToString(), out realValue))
                        {
                            realChannels.Add(new Channel { Name = realName, Value = realValue });
                        }
                    }
                }
            }

            return realChannels;
        }

        protected override void Dispose(bool disposing)
        {
            temperatureManager?.Dispose();
            base.Dispose(disposing);
        }
    }
}