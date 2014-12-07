using Fixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixtures.Data.DAL
{
    internal class FixtureContext : DbContext
    {
        public FixtureContext()
            :base("Fixture")
        {

        }
        public DbSet<ArtNetFixtureEntity> ArtNetFixtures { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
