using System;
using System.ComponentModel.DataAnnotations;

namespace PreorderPlatform.Services.Utility
{
    public class AllowedRoleIdsAttribute : ValidationAttribute
    {
        private readonly Guid[] _allowedRoleIds;

        public AllowedRoleIdsAttribute(params Guid[] allowedRoleIds)
        {
            _allowedRoleIds = allowedRoleIds;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Role ID cannot be null");
            }

            Guid roleId = (Guid)value;

            if (Array.IndexOf(_allowedRoleIds, roleId) == -1)
            {
                return new ValidationResult("Invalid Role ID. Allowed values are 'BUSINESS_OWNER', 'BUSINESS_STAFF', or 'CUSTOMER'.");
            }

            return ValidationResult.Success;
        }
    }
}