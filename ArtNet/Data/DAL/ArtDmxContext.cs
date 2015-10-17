using ArtNet.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtNet.Data.DAL
{
    class ArtDmxContext : DbContext
    {
        public ArtDmxContext()
            :base(@"Data Source=(localdb)\ProjectsV12;Initial Catalog=HomeController;Integrated Security=SSPI;")
        {

        }

        public DbSet<ArtDmxEntity> ArtDmxEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
