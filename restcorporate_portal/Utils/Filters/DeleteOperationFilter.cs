using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace restcorporate_portal.Utils.Filters
{
    public class DeleteOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //if (context.ApiDescription.HttpMethod == "DELETE" && context.MethodInfo.Name == "Delete")
            //{
                foreach (var parameter in context.ApiDescription.ParameterDescriptions)
                {
                    if (parameter.RouteInfo == null && operation.Parameters.Count > 0)
                    {
                        operation.Parameters.Single(x => x.Name.Equals(parameter.Name)).Required = false;
                    }
                }
                return;
            //}
        }
    }
}
