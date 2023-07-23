using System.Text.Json.Serialization;

namespace PreOrderPlatform.Entity.Enum.Payment
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentMethod
    {
        Momo,
        VNPay,
        BankTransfer,
        CashOnDelivery
    }
}
