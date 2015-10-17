using System.Collections.Generic;

namespace EventBus.Messaging
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Fixture> Fixtures { get; set; }
    }
}