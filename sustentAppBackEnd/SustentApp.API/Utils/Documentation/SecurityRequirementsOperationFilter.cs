using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SustentApp.API.Utils.Documentation;

public class SecurityRequirementsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var requiredScopes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>();

        if (requiredScopes.Any())
        {
            operation.Responses.Add("401", new OpenApiResponse
            {
                Description = "User not authenticated."
            });

            operation.Responses.Add("403", new OpenApiResponse
            {
                Description = "User not authorized to perform this operation."
            });

            operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "bearer",
                            Name = "bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
        }
    }
}
