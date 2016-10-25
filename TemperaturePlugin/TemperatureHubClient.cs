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
        /// <summary>
        /// Initializes a new instance of the <see cref="TemperatureHubClient"/> class.
        /// </summary>
        public TemperatureHubClient()
            : base("Temperature", "Temperature")
        {
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
                    UpdateReceived(message);
                    break;
            }
        }

        void UpdateReceived(Message message)
        {
            if (message.Values != null)
            {
                var channels = GetChannelsFromMessage(message.Values);

                // send values to art net
                TemperatureManager.UpdateTargetTemperatures(channels);
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
    }
}