using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PreorderPlatform.Service.ViewModels.ApiResponse;

public static class GetValidationError
{
    public static Dictionary<string, string[]> FromModelState(ModelStateDictionary modelState)
    {
        var validationErrors = new Dictionary<string, string[]>();

        foreach (var keyValuePair in modelState)
        {
            if (keyValuePair.Value.Errors.Any())
            {
                validationErrors[keyValuePair.Key] = keyValuePair.Value.Errors.Select(e => e.ErrorMessage).ToArray();
            }
        }

        return validationErrors;
    }
}