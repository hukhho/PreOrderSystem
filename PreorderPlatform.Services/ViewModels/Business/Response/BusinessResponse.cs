namespace PreOrderPlatform.Service.ViewModels.Business.Response
{
    public class BusinessResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? LogoUrl { get; set; }
        public Guid OwnerId { get; set; }
        public bool IsVerified { get; set; }
        public bool Status { get; set; }

    }
}
