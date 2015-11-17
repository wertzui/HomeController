using ArtNet.Common;
using System.Threading.Tasks;

namespace ArtNet.Data
{
    public interface IPersistence
    {
        Task<ArtDmxPackage> GetAsync(short Universe);

        Task UpdateAsync(ArtDmxPackage package);
    }
}