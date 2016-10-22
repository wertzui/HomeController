using System.Threading.Tasks;
using System.Web.Http;

namespace TemperaturePlugin
{
    public class TemperatureController : ApiController
    {
        [HttpPost]
        public Task Post(TemperatureUpdate value)
            => TemperatureManager.Update(value.name, value.temperature, value.humidity);
    }

    public class TemperatureUpdate
    {
        public string name { get; set; }
        public double temperature { get; set; }
        public double humidity { get; set; }
    }
}