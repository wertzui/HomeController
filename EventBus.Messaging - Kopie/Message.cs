using System;
using System.Collections.Generic;

namespace EventBus.Messaging
{
    public class Message
    {
        public string Sender { get; set; }
        public string Target { get; set; }
        public DateTime Time { get; set; }
        public IDictionary<string, object> Values { get; set; }
    }
}