using Fixtures.Data.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixtures.Data.DAL
{
    internal class FixtureContext : DbContext
    {
        public FixtureContext()
            :base()
        {

        }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptions builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\ProjectsV12;Database=HomeController;Trusted_Connection=True;");
        }

    }
}
