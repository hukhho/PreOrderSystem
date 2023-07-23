using System.ComponentModel.DataAnnotations;

namespace PreOrderPlatform.Service.ViewModels.User.Request
{
    public class GetResetPasswordToken
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}
