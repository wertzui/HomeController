using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtNet.Data.DAL
{
    class ArtDmxInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ArtDmxContext>
    {
        protected override void Seed(ArtDmxContext context)
        {
            base.Seed(context);
        }
    }
}
