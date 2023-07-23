using PreOrderPlatform.Entity.Enum.User;

namespace PreOrderPlatform.Service.ViewModels.User.Request
{
    public class UserPatchRequest
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Ward { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public UserStatus? Status { get; set; }
    }
}
