using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace EventBus
{
    class Startup
    {
        /// <summary>
        /// Configures the SignalR hosting environment.
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                // Since this is a bus system, we actually want requests from cross domains
                map.UseCors(CorsOptions.AllowAll);

                var hubConfiguration = new HubConfiguration
                {
                    // As this is used an house, there is not much risk involveld when exposing errors,
                    // but it is must easier to debug
                    EnableDetailedErrors = true
                };

                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch is already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}