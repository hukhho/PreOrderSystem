namespace PreorderPlatform.Services.ViewModels.Business.Request
{
    public class BusinessSearchRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Guid? OwnerId { get; set; }
        public bool? Status { get; set; }
    }
}