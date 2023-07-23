namespace PreOrderPlatform.Service.ViewModels.Category
{
    public class CategoryUpdateViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
    }
}
