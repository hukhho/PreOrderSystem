using System.Text.Json.Serialization;

namespace PreOrderPlatform.Entity.Enum.Payment
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded,
        Cancelled
    }
}
