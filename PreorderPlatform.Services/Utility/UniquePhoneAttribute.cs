using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.Services.UserServices;

namespace PreorderPlatform.Services.Utility
{
    public class UniquePhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Phone number cannot be null");
            }

            string phone = value.ToString();

            var userService = (IUserService)validationContext.GetService(typeof(IUserService));

            if (!IsPhoneUnique(userService, phone).Result)
            {
                return new ValidationResult("Phone number already exists");
            }

            return ValidationResult.Success;
        }

        private async Task<bool> IsPhoneUnique(IUserService userService, string phone)
        {
            try
            {
                var isUnique = await userService.IsPhoneUniqueAsync(phone);
                return isUnique;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while checking uniqueness of phone number {phone}.", ex);
            }
        }
    }
}
