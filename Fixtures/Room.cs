using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixtures
{
    public class Room
    {
        public string Name { get; set; }
        public ObservableCollection<IArtNetWhiteFixture> ArtNetWhiteFixtures { get; set; }
        public ObservableCollection<IArtNetRGBFixture> ArtNetRGBFixtures { get; set; }
    }
}
