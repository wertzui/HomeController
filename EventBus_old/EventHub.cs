using EventBus.Messaging;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;

namespace EventBus
{
    public class EventHub : Hub
    {
        public void Send(Message message)
        {
            Clients.AllExcept(Context.ConnectionId).Receive(message);
        }

        public void RequestConfiguration()
        {
            var room = new Location
            {
                Id = 1,
                Name = "Wohnzimmer",
                Fixtures = new List<Fixture>
                {
                    new Fixture
                    {
                        Channels = new Dictionary<string, ChannelType>{ { "Dimmer", ChannelType.Percentage } },
                        Id = 1,
                        Name = "Deckeenlicht",
                        RoomId = 1
                    }
                }
            };
            var rooms = new[] { room };
            var config = new Message
            {
                Sender = "EventBus.Config",
                Target = Context.ConnectionId,
                Time = DateTime.Now,
                Values = new Dictionary<string, object> { { "Rooms", rooms } }
            };
            Clients.Caller.UpdateConfiguration(config);
        }
    }
}