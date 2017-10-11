using EventBus.Messaging;
using Plugins.Common;
using Plugins.Common.Fixtures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotionPlugin
{
    /// <summary>
    /// Hub client for the fixture register that handles the loading and update of all fixtures.
    /// </summary>
    public class MotionHubClient : HubClientBase
    {
        private readonly MotionManager motionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MotionHubClient"/> class.
        /// </summary>
        public MotionHubClient()
            : base("Motion", "Motion")
        {
            motionManager = new MotionManager(this);
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

        private async Task UpdateReceived(Message message)
        {
            if (message.Values != null)
            {
                var channels = GetChannelsFromMessage(message.Values);

                // send values to art net
                await motionManager.UpdateTargetMotions(channels).ConfigureAwait(false);
            }
        }
        
        private static IEnumerable<Channel> GetChannelsFromMessage(IEnumerable<dynamic> channels)
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
            motionManager?.Dispose();
            base.Dispose(disposing);
        }
    }
}