using EventBus.Messaging;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Common
{
    public abstract class HubClient : IDisposable
    {
        private HubConnection hubConnection;
        private IHubProxy eventHubProxy;

        public string TargetFilter { get; set; }

        public string Url { get; private set; }

        public string Sender { get; set; }

        public HubClient(string url, string sender)
        {
            Url = url;
            Sender = sender;
        }

        public async Task StartAsync()
        {
            hubConnection = new HubConnection(Url);
            eventHubProxy = hubConnection.CreateHubProxy("EventHub");
            eventHubProxy.On<Message>("Receive",
                async message => await FilterMessage(message, async () => await OnReceive(message)));
            eventHubProxy.On<Message>("UpdateConfiguration",
                async message => await FilterMessage(message, async () => await OnUpdateConfiguration(message.Values["Locations"] as IEnumerable<Location>)));
            await hubConnection.Start();
            await eventHubProxy.Invoke<Message>("RequestConfiguration");
        }

        private static IDictionary<string, object> AsDictionary(object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            if (source == null)
                return new Dictionary<string, object>();

            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
        }

        public async Task<Message> SendAsnyc(object values, string target = null)
        {
            return await SendAsnyc(AsDictionary(values), target);
        }

        public async Task<Message> SendAsnyc(IDictionary<string, object> values, string target = null)
        {
            var message = new Message
            {
                Sender = Sender,
                Target = target,
                Time = DateTime.Now,
                Values = values
            };

            return await eventHubProxy.Invoke<Message>("Send", message);
        }

        private async Task FilterMessage(Message message, Action onSuccess)
        {
            if (TargetFilter == null || TargetFilter == message.Target)
                onSuccess();
        }

        protected virtual async Task OnUpdateConfiguration(IEnumerable<Location> locations)
        {
        }

        protected virtual async Task OnReceive(Message message)
        {
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        hubConnection.Stop();
                    }
                    catch { }
                    try
                    {
                        hubConnection.Dispose();
                    }
                    catch { }
                }
                eventHubProxy = null;
                hubConnection = null;

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}