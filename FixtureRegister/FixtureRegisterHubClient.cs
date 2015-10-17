using EventBus.Messaging;
using Plugins.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FixtureRegister
{
    /// <summary>
    /// Hub client for the fixture register that handles the loading and update of all fixtures.
    /// </summary>
    public class FixtureRegisterHubClient : HubClientBase
    {
        readonly RoomManager roomManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixtureRegisterHubClient"/> class.
        /// </summary>
        public FixtureRegisterHubClient()
            : base(nameof(FixtureRegister), nameof(FixtureRegister))
        {
            roomManager = new RoomManager(this);
        }

        /// <summary>
        /// Called before the connection is started.
        /// </summary>
        /// <returns></returns>
        protected override async Task OnBeforeConnectionStart()
        {
            await roomManager.StartAsync();
            await base.OnBeforeConnectionStart();
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
                case MethodType.Get:
                    await GetFixtures(message.Sender);
                    break;

                case MethodType.Update:
                    UpdateChannels(message.Values);
                    break;
            }
        }

        /// <summary>
        /// Updates the channels in all rooms with the new values.
        /// </summary>
        /// <param name="values">The values.</param>
        void UpdateChannels(IEnumerable<dynamic> values)
        {
            roomManager.UpdateChannels(values);
        }

        /// <summary>
        /// Gets the all fixtures.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <returns></returns>
        async Task GetFixtures(string sender)
        {
            await SendAsync(roomManager.Rooms, MethodType.Update, sender);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue && disposing)
            {
                roomManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}