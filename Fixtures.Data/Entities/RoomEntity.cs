using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fixtures.Data.Entities
{
    internal class RoomEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ArtNetFixtureEntity> ArtNetFixtures { get; set; }
    }
}
