namespace PreOrderPlatform.Service.ViewModels.Product.Request
{
    public class ProductSearchRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? BusinessId { get; set; }
    }
}
