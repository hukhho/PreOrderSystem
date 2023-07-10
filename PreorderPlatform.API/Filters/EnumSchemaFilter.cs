namespace PreorderPlatform.API.Filters
{
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;

    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();
                var enumValues = Enum.GetValues(context.Type);
                foreach (var enumValue in enumValues)
                {
                    schema.Enum.Add(new OpenApiString(enumValue.ToString()));
                }

                schema.Type = "string";
                schema.Format = null;
            }
        }
    }
}
