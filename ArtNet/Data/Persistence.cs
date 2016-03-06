using ArtNet.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArtNet.Data
{
    public class Persistence : IPersistence
    {
        private static string DataFolder = Path.GetDirectoryName((Assembly.GetExecutingAssembly().Location)) + "\\ArtNet\\";
        static string GeneratFullPath(short universe) => $"{DataFolder}\\{universe}.json";
        public Persistence()
        {

        }

        public async Task<ArtDmxPackage> GetAsync(short universe)
        {
            try
            {
                string fileContent;
                using (var reader = File.OpenText(GeneratFullPath(universe)))
                {
                    fileContent = await reader.ReadToEndAsync();
                }
                return JsonConvert.DeserializeObject<ArtDmxPackage>(fileContent);
            }
            catch (Exception)
            {
                return new ArtDmxPackage { Universe = universe };
            }
        }

        public async Task UpdateAsync(ArtDmxPackage package)
        {
            if (package == null)
                throw new ArgumentNullException(nameof(package));

            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);

            var fileContent = JsonConvert.SerializeObject(package, Formatting.Indented);
            var fullPath = GeneratFullPath(package.Universe);

            using (var writer = File.CreateText(fullPath))
            {
                await writer.WriteAsync(fileContent);
            }
        }

    }
}
