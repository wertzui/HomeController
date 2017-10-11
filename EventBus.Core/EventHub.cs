using EventBus.Messaging;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBus.Core
{
    /// <summary>
    /// The EventHub is the central hub in the bus that is in charge of redistributing all messages it receives.
    /// </summary>
    public class EventHub : Hub
    {
        /// <summary>
        /// Forwards a message to the specified target Receive method.
        /// </summary>
        /// <param name="message">The message to forward.</param>
        /// <remarks>The methos id called "Send" so any client can use "Hub.Send(...)" which makes it clearer what it is doing.</remarks>
        public Task Send(Message message)
        {
            Console.WriteLine("Message: " + JsonConvert.SerializeObject(message, Formatting.Indented));
            if(message.Target != null)
                return Clients.Group(message.Target).InvokeAsync("Receive", message);

            return Clients.AllExcept(new[] { Context.ConnectionId }).InvokeAsync("Receive", message);
        }

        /// <summary>
        /// Allows plugins to join a group.
        /// That way they will only get messages intended for that group
        /// Plugins will join the group that equals their TargetFilter property.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns></returns>
        public Task JoinGroup(string groupName)
        {
            return Groups.AddAsync(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// Allows plugins to leave a group.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns></returns>
        public Task LeaveGroup(string groupName)
        {
            return Groups.RemoveAsync(Context.ConnectionId, groupName);
        }
    }
}