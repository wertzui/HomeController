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
    internal class ArtDmxContext : DbContext
    {
        public ArtDmxContext()
            :base("ArtDmxContext")
        {

        }

        public DbSet<ArtDmxEntity> ArtDmxEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
