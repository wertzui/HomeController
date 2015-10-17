using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace EventBus.Messaging
{
    /// <summary>
    /// Represents a message that can be sent using the event bus
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the method type of this message.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public MethodType Method { get; set; }

        /// <summary>
        /// Gets or sets the name of the sender.
        /// </summary>
        /// <value>
        /// The sender.
        /// </value>
        public string Sender { get; set; }

        /// <summary>
        /// Gets or sets the name of the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the time when this message was created.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the values that are contained inside this message.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public IEnumerable<dynamic> Values { get; set; }
    }
}