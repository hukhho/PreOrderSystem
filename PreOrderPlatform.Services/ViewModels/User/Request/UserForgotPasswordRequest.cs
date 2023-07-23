using System.ComponentModel.DataAnnotations;

namespace PreOrderPlatform.Service.ViewModels.User.Request
{ 
    public class UserForgotPasswordRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}
