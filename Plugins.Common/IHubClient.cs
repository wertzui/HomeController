using EventBus.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugins.Common
{
    /// <summary>
    /// Interface for hub clients.
    /// </summary>
    public interface IHubClient
    {
        /// <summary>
        /// Sends a new message to the event bus.
        /// </summary>
        /// <param name="values">The values that will be sent.</param>
        /// <param name="method">The method type of this message.</param>
        /// <param name="target">The target of this message.</param>
        /// <returns></returns>
        Task<Message> SendAsync(IEnumerable<dynamic> values, MethodType method, string target = null);
    }
}