using EventBus.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;

namespace EventBus.Core.Controllers
{
    [Route("[controller]")]
    public class BusController : Controller
    {
        private readonly IHubContext<EventHub> hubContext;

        public BusController(IHubContext<EventHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        [HttpPost]
        public void Post([FromBody]Message message)
        {
            Console.WriteLine("Message: " + JsonConvert.SerializeObject(message, Formatting.Indented));
            if (message.Target != null)
                hubContext.Clients.Group(message.Target).InvokeAsync("Receive", message);

            hubContext.Clients.All.InvokeAsync("Receive", message);
        }
    }
}