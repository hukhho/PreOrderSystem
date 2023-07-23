using System.ComponentModel.DataAnnotations;
using PreOrderPlatform.Service.Utility;

namespace PreOrderPlatform.Service.ViewModels.User.Request
{
    public class UserCreateRequest
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than {1} characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than {1} characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [UniquePhone]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [UniqueEmail]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string? Password { get; set; }

        //[Required(ErrorMessage = "Role ID is required.")]
        ////[AllowedRoleIds("2", "3", "4", ErrorMessage = "Invalid Role ID. Allowed values are 2, 3, or 4.")]
        //public Guid? RoleId { get; set; }
        [Required(ErrorMessage = "Role name is required.")]
        [AllowedRoleNames("CUSTOMER", "BUSINESS_OWNER", "BUSINESS_STAFF", ErrorMessage = "Invalid role name. Allowed values are CUSTOMER, BUSINESS_OWNER, or BUSINESS_STAFF.")]
        public string? RoleName { get; set; }

    }
}