using PreOrderPlatform.Entity.Enum.User;
using PreOrderPlatform.Service.ViewModels.Business.Response;

namespace PreOrderPlatform.Service.ViewModels.User.Response
{
    public class UserResponse
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

        public string? RoleName { get; set; }

        public Guid? BusinessId { get; set; }
        public string? BusinessName { get; set; }

        public BusinessResponse? Business { get; set; }

        public UserStatus? Status { get; set; }
    }
}
