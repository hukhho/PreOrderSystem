using System.ComponentModel.DataAnnotations;

namespace PreOrderPlatform.Service.ViewModels.User.Request
{
    public class UserUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        public string? FirstName { get; set; }

        [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
        public string? LastName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? Phone { get; set; }

        [StringLength(300, ErrorMessage = "Address cannot be longer than 300 characters.")]
        public string? Address { get; set; }

        [StringLength(100, ErrorMessage = "Ward cannot be longer than 100 characters.")]
        public string? Ward { get; set; }

        [StringLength(100, ErrorMessage = "District cannot be longer than 100 characters.")]
        public string? District { get; set; }

        [StringLength(100, ErrorMessage = "Province cannot be longer than 100 characters.")]
        public string? Province { get; set; }
    }
}