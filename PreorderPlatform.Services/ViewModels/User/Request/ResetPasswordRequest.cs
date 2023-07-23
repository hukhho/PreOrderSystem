using System.ComponentModel.DataAnnotations;

namespace PreOrderPlatform.Service.ViewModels.User.Request
{
    public class ResetPasswordRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
        //[Required(ErrorMessage = "Reset token is required.")]
        //public string ResetToken { get; set; }
        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
