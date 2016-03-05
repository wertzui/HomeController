using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fixtures.Data.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Fixture> Fixtures { get; set; }
    }
}
