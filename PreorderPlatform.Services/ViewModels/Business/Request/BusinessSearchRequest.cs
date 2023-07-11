namespace PreorderPlatform.Services.ViewModels.Business.Request
{
    public class BusinessSearchRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? OwnerId { get; set; }
        public bool? Status { get; set; }
    }
}