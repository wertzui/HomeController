using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixtures.Data
{
    public class Persistance
    {
        DAL.FixtureContext context = new DAL.FixtureContext();
        public IEnumerable<string> GetRooms()
        {
            return context.Rooms.Select(r => r.Name).OrderBy(r => r).ToList();
        }

        public Room GetRoom(string name)
        {
            var row = context.Rooms.FirstOrDefault(r => r.Name == name);
            
            return null;
        }

    }
}
