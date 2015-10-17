using ArtNet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtNet.Data
{
    public interface IPersistence
    {
        ArtDmxPackage Get(short Universe);

        Task UpdateAsync(ArtDmxPackage package);
    }
}
