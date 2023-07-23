using System.Text.Json.Serialization;

namespace PreOrderPlatform.Entity.Enum.Order
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        Pending,
        Processing,
        Complete,
        Cancelled
    }
}
