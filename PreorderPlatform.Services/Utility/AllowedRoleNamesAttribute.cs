using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Utility
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
