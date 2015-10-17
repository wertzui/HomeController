using EventBus.Messaging;
using Microsoft.AspNet.SignalR;

namespace EventBus
{
    public class EventHub : Hub
    {
        public void Send(Message message)
        {
            Clients.AllExcept(Context.ConnectionId).Receive(message);
        }
    }
}