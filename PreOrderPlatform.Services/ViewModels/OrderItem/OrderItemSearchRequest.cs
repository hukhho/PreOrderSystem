using Newtonsoft.Json;

namespace PreOrderPlatform.Service.ViewModels.OrderItem
{
    public class OrderItemSearchRequest
    {
        [JsonIgnore]
        public Guid OrderId { get; set; }
    }
}
