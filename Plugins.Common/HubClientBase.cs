using EventBus.Messaging;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugins.Common
{
    /// <summary>
    /// Base class for all plugin clients.
    /// </summary>
    public abstract class HubClientBase : IHubClient, IDisposable
    {
        private HubConnection hubConnection;
        private IHubProxy eventHubProxy;

        /// <summary>
        /// Only Messages containing this Target or null as Target will be delivered to this Client.
        /// The client will join the group with the same name as the target filter on the event hub.
        /// </summary>
        public string TargetFilter { get; }

        /// <summary>
        /// Gets or sets the URL of the event hub.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; internal set; }

        /// <summary>
        /// Gets or sets the name of the sender.
        /// Normally this should be the name of the plugin.
        /// </summary>
        /// <value>
        /// The sender.
        /// </value>
        public string Sender { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HubClientBase" /> class.
        /// </summary>
        /// <param name="sender">The sender. Normally this should be the name of the plugin.</param>
        /// <param name="targetFilter">Only Messages containing this Target or null as Target will be delivered to this Client.</param>
        protected HubClientBase(string sender, string targetFilter = null)
        {
            Sender = sender;
            TargetFilter = targetFilter;
        }

        /// <summary>
        /// Starts the plugin and connects to the hub.
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            Console.WriteLine("Starting Hub {0}", Sender);
            await OnBeforeConnectionStart();
            hubConnection = new HubConnection(Url);
            hubConnection.JsonSerializer.Converters.Add(new StringEnumConverter());
            eventHubProxy = hubConnection.CreateHubProxy("EventHub");
            eventHubProxy.On<Message>("Receive",
                async message => await OnReceiveAsync(message));
            await hubConnection.Start();
            await JoinGroup(TargetFilter);
            Console.WriteLine($"Hub {Sender} started with Group {TargetFilter}");
            await SendAsync(null, MethodType.StatusStarted);
        }

        /// <summary>
        /// Sends a new message to the event bus.
        /// </summary>
        /// <param name="values">The values that will be sent.</param>
        /// <param name="method">The method type of this message.</param>
        /// <param name="target">The target of this message.</param>
        /// <returns></returns>
        public async Task<Message> SendAsync(IEnumerable<dynamic> values, MethodType method, string target = null)
        {
            var message = new Message
            {
                Method = method,
                Sender = Sender,
                Target = target,
                Time = DateTime.Now,
                Values = values
            };

            Console.WriteLine("Sending " + JsonConvert.SerializeObject(message, Formatting.Indented));

            return await eventHubProxy.Invoke<Message>("Send", message);
        }

        /// <summary>
        /// Joins the given group in the event hub.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns></returns>
        private Task JoinGroup(string groupName)
        {
            return eventHubProxy.Invoke<Message>(nameof(JoinGroup), groupName);
        }

        /// <summary>
        /// Leaves the given group in the event hub.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns></returns>
        private Task LeaveGroup(string groupName)
        {
            return eventHubProxy.Invoke<Message>(nameof(LeaveGroup), groupName);
        }

        /// <summary>
        /// Called when a new message from the event bus is received.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected abstract Task OnReceiveAsync(Message message);

        /// <summary>
        /// Called while the plugin is starting, but before the connection to the event hub is initialized.
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnBeforeConnectionStart() { }

        #region IDisposable Support

        protected bool disposedValue = false; // To detect redundant calls

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
                    SendAsync(null, MethodType.StatusStopped).Wait();
                    try
                    {
                        hubConnection.Stop();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    try
                    {
                        hubConnection.Dispose();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                eventHubProxy = null;
                hubConnection = null;

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