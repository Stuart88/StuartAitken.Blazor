using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace StuartAitken.Blazor.Server.ActionFilters
{
    public class AuthHeaderSwaggerFilter : IOperationFilter
    {
        #region Public Methods

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                // If [AllowAnonymous] is not applied or [Authorize] or Custom Authorization filter is applied on either the endpoint or the controller
                if (
                    !context.ApiDescription
                        .CustomAttributes()
                        .Any((a) => a is AllowAnonymousAttribute)
                    && (
                        context.ApiDescription.CustomAttributes().Any((a) => a is AdminAuthorise)
                        || descriptor.ControllerTypeInfo.GetCustomAttribute<AdminAuthorise>()
                            != null
                    )
                )
                {
                    if (operation.Security == null)
                        operation.Security = new List<OpenApiSecurityRequirement>();

                    var scheme = new OpenApiSecuritySchemeReference("Bearer", context.Document);

                    operation.Security.Add(
                        new OpenApiSecurityRequirement { [scheme] = new List<string>() }
                    );
                }
            }
        }

        #endregion Public Methods
    }
}
