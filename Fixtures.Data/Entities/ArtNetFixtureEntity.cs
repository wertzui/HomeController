using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixtures.Data.Entities
{
    internal class ArtNetFixtureEntity : FixtureEntity
    {
        public short StartAddress { get; set; }
        public short Universe { get; set; }
    }
}
