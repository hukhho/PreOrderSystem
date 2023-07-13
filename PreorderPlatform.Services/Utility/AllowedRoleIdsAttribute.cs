using System;
using System.ComponentModel.DataAnnotations;

namespace PreorderPlatform.Services.Utility
{
    public class AllowedRoleIdsAttribute : ValidationAttribute
    {
        private readonly int[] _allowedRoleIds;

        public AllowedRoleIdsAttribute(params int[] allowedRoleIds)
        {
            _allowedRoleIds = allowedRoleIds;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Role ID cannot be null");
            }

            int roleId = (int)value;

            if (Array.IndexOf(_allowedRoleIds, roleId) == -1)
            {
                return new ValidationResult("Invalid Role ID. Allowed values are 2, 3, or 4.");
            }

            return ValidationResult.Success;
        }
    }
}