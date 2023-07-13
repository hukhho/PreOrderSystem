using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.Services.UserServices;

namespace PreorderPlatform.Services.Utility
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Email cannot be null");
            }

            string email = value.ToString();

            var userService = (IUserService)validationContext.GetService(typeof(IUserService));

            if (!IsEmailUnique(userService, email).Result)
            {
                return new ValidationResult("Email already exists");
            }

            return ValidationResult.Success;
        }

        private async Task<bool> IsEmailUnique(IUserService userService, string email)
        {
            try
            {
                var isUnique = await userService.IsEmailUniqueAsync(email);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while checking uniqueness of email {email}.", ex);
            }
        }
    }
}