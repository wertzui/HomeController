﻿using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;
using Microsoft.Owin.Cors;
using EventBus;

namespace SignalRSelfHost
{
    class Program
    {
        /// <summary>
        /// Starts the bus in a Console host.
        /// </summary>
        static void Main()
        {
            var url = "http://*:1906";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Event Bus Server running at {0}", url);
                Console.ReadLine();
            }
        }
    }
}