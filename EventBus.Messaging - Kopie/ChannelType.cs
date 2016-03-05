using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventBus.Messaging
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChannelType
    {
        Switch,
        Percentage,
        Number,
        Direct,
        String
    }
}