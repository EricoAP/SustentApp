using Microsoft.OpenApi.Models;
using SustentApp.DataTransfer.Utils.Errors.Response;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SustentApp.API.Utils.Documentation;

public class DefaultOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var errorSchema = context.SchemaGenerator.GenerateSchema(typeof(ErrorResponse), context.SchemaRepository);
        operation.Responses.Add("400", new OpenApiResponse
        {
            Description = "Invalid request. Check the details in the response.",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = errorSchema
                }
            }
        });
        operation.Responses.Add("404", new OpenApiResponse
        {
            Description = "Resource not found.",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = errorSchema
                }
            }
        });
        operation.Responses.Add("500", new OpenApiResponse
        {
            Description = "Internal server error.",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = errorSchema
                }
            }
        });
    }
}
