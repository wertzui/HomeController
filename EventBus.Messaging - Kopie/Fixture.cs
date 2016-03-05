using System.Collections.Generic;

namespace EventBus.Messaging
{
    public class Fixture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomId { get; set; }
        public Location Location { get; set; }

        public IDictionary<string, ChannelType> Channels { get; set; }
    }
}