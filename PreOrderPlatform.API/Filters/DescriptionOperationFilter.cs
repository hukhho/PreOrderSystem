using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PreOrderPlatform.API.Filters
{
    public class DescriptionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var queryParameters = context.ApiDescription.ParameterDescriptions
                .Where(p => p.Source.Id == "Query" && p.ModelMetadata.ContainerType != null)
                .Select(p => p.ModelMetadata.ContainerType.GetProperty(p.ModelMetadata.PropertyName));

            foreach (var property in queryParameters)
            {
                if (property != null)
                {
                    var descriptionAttribute = property.GetCustomAttribute<DescriptionAttribute>();
                    if (descriptionAttribute != null)
                    {
                        var camelCasePropertyName = JsonNamingPolicy.CamelCase.ConvertName(property.Name);
                        var openApiParameter = operation.Parameters.FirstOrDefault(p => p.Name == camelCasePropertyName);
                        if (openApiParameter != null)
                        {
                            openApiParameter.Description = descriptionAttribute.Description;
                        }
                    }
                }
            }
        }
    }
}
