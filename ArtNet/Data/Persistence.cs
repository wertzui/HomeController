using ArtNet.Common;
using ArtNet.Data.DAL;
using ArtNet.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArtNet.Data
{
    public class Persistence : IPersistence
    {
        private ArtDmxContext context;

        public Persistence()
            : this(new ArtDmxContext())
        {

        }

        internal Persistence(ArtDmxContext context)
        {
            this.context = context;
        }

        public ArtDmxPackage Get(short universe)
        {
            var entity = context.ArtDmxEntities.FirstOrDefault(e => e.Universe == universe);
            var data = DataConverter.GetBytesFromEntity(entity);
            var package = new ArtDmxPackage(data);
            package.Universe = universe;
            if (entity != null)
                package.Sequence = entity.Sequence;

            return package;
        }

        public async Task UpdateAsync(ArtDmxPackage package)
        {
            if (package == null)
                throw new ArgumentNullException("package");

            var entity = context.ArtDmxEntities.FirstOrDefault(r => r.Universe == package.Universe);

            if (entity == null)
            {
                entity = context.ArtDmxEntities.Add(context.ArtDmxEntities.Create());
                entity.Universe = package.Universe;
            }

            DataConverter.SetBytesToEntity(entity, package.Data);
            entity.Sequence = package.Sequence;

            await context.SaveChangesAsync();
        }
    }
}
