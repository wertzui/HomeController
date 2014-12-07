using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixtures.Data.Entities
{
    internal class FixtureEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomId { get; set; }
        public RoomEntity Room { get; set; }
        public string Type { get; set; }
    }
}
