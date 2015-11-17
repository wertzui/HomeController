using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixtures.Data.Entities
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
