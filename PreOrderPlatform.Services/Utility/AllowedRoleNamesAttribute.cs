using System.ComponentModel.DataAnnotations;

namespace PreOrderPlatform.Service.Utility
{
    public class AllowedRoleNamesAttribute : ValidationAttribute
    {
        private readonly string[] _allowedRoleNames;

        public AllowedRoleNamesAttribute(params string[] allowedRoleNames)
        {
            _allowedRoleNames = allowedRoleNames;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext? validationContext)
        {
            var roleName = value as string;

            if (_allowedRoleNames.Contains(roleName, StringComparer.OrdinalIgnoreCase))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? $"Invalid role name. Allowed values are {string.Join(", ", _allowedRoleNames)}.");
        }
    }
}
