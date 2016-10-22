using Owin;
using System.Web.Http;

namespace TemperaturePlugin
{
    class Startup
    {
        /// <summary>
        /// Configures the SignalR hosting environment.
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            //app.Map("", map =>
            //{
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );

            app.UseWebApi(config);
            //});
        }
    }
}