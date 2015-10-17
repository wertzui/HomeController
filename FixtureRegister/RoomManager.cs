using EventBus.Messaging;
using Plugins.Common;
using Plugins.Common.Fixtures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace FixtureRegister
{
    /// <summary>
    /// Manages the roomes and all updates to any values within all those rooms.
    /// </summary>
    class RoomManager : IDisposable
    {
        /// <summary>
        /// Gets the rooms.
        /// </summary>
        /// <value>
        /// The rooms.
        /// </value>
        internal IEnumerable<Room> Rooms { get; private set; }
        bool roomsChanged;
        const int updateIntervall = 10000;
        Timer configUpdateTimer;
        FixtureConfigReaderWriter config = new FixtureConfigReaderWriter();
        IHubClient hub;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomManager"/> class.
        /// </summary>
        /// <param name="hub">The hub that is used to send updates that may occur in any channels of the rooms.</param>
        public RoomManager(IHubClient hub)
        {
            this.hub = hub;
        }

        /// <summary>
        /// Loads the rooms and starts looking for updates
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            Rooms = await config.ReadConfigAsync();
            StartUpdateTimer();
        }

        /// <summary>
        /// Starts the update timer.
        /// </summary>
        void StartUpdateTimer()
        {
            configUpdateTimer = new Timer(updateIntervall);
            configUpdateTimer.Elapsed += ConfigUpdateTimer_Elapsed;
            configUpdateTimer.Start();
        }

        /// <summary>
        /// Handles the Elapsed event of the ConfigUpdateTimer control.
        /// Saves changed to the underlying config and sends an update to the bus if any changes have occured.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        async void ConfigUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (roomsChanged)
            {
                roomsChanged = false;
                await hub.SendAsync(Rooms, MethodType.Update);
                await config.WriteConfigAsync(Rooms);
            }
        }

        /// <summary>
        /// Updates the channels with values from a received message.
        /// </summary>
        /// <param name="channels">The channels from the Values property of a message.</param>
        internal void UpdateChannels(IEnumerable<dynamic> channels)
        {
            if (channels != null)
            {
                var realChannels = new List<Channel>();
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
                foreach (var room in Rooms)
                {
                    room.UpdateChannels(realChannels);
                }
                roomsChanged = true;
            }
        }

        #region IDisposable Support

        bool disposedValue; // To detect redundant calls

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
                    configUpdateTimer.Stop();
                    configUpdateTimer.Elapsed -= ConfigUpdateTimer_Elapsed;
                    configUpdateTimer.Dispose();
                }

                hub = null;

                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}