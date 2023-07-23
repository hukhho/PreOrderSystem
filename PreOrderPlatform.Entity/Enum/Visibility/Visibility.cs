using System.Text.Json.Serialization;

namespace PreOrderPlatform.Entity.Enum.Visibility
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Visibility
    {
        Public,
        Private
    }
}
