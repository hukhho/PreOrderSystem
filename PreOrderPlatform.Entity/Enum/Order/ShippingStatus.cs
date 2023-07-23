using System.Text.Json.Serialization;

namespace PreOrderPlatform.Entity.Enum.Order
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ShippingStatus
    {
        Pending,
        Shipped,
        InTransit,
        Delivered,
        Cancelled
    }
}
