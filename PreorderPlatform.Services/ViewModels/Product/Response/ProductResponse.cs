namespace PreOrderPlatform.Service.ViewModels.Product.Response
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryName { get; set; }
        public bool? Status { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? BusinessId { get; set; }
    }
}
