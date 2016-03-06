using ArtNet.Common;
using ArtNet.Fascade;
using EventBus.Messaging;
using Plugins.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtNet
{
    /// <summary>
    /// Client to send values to ArtNet
    /// </summary>
    public class ArtNetHubClient : HubClientBase
    {
        readonly ArtNetFascade fascade;
        const string artNetHost = "192.168.0.200";
        const short universe = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtNetHubClient"/> class.
        /// </summary>
        public ArtNetHubClient()
            : base(nameof(ArtNet), nameof(ArtNet))
        {
            fascade = new ArtNetFascade(universe, artNetHost);
        }

        /// <summary>
        /// Called when a message is received.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected override Task OnReceiveAsync(Message message)
        {
            switch (message.Method)
            {
                case MethodType.Update:
                    UpdateReceived(message);
                    break;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Send message values to ArtNet.
        /// </summary>
        /// <param name="message">The message.</param>
        void UpdateReceived(Message message)
        {
            if (message.Values != null)
            {
                var pairs = GetValuesFromMessage(message);

                // send values to art net
                fascade.Set(pairs);
            }
        }

        /// <summary>
        /// Extracts key value pairs from the message values.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        static IEnumerable<AddressValuePair> GetValuesFromMessage(Message message)
        {
            // try to get address value pairs from the channels
            var pairs = new List<AddressValuePair>();
            foreach (var channel in message.Values)
            {
                var name = channel.Name;
                var value = channel.Value;

                if (name != null && value != null)
                {
                    short address;
                    if (short.TryParse(name.ToString(), out address))
                    {
                        byte dmxValue;
                        if (byte.TryParse(value.ToString(), out dmxValue))
                        {
                            pairs.Add(new AddressValuePair(address, dmxValue));
                        }
                    }
                }
            }

            return pairs;
        }
    }
}