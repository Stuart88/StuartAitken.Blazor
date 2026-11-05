using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StuartAitken.Blazor.Server.Controllers;
using StuartAitken.Blazor.Server.DataService;
using StuartAitken.Blazor.Server.Helpers;
using StuartAitken.Blazor.Server.Security;
using StuartAitken.Blazor.Shared.Models;

namespace StuartAitken.Blazor.Server.ActionFilters
{
    public class AdminAuthorise : ActionFilterAttribute
    {
        #region Private Fields

        private const string AdminSecureData = "ADMIN";

        #endregion Private Fields

        #region Public Methods

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            bool isAdmin = false;

            try
            {
                var dataService = context.HttpContext.RequestServices.GetService<SecureDataService>();
                var controller = context.Controller as CustomControllerBase;

                if (dataService != null && controller != null)
                {
                    string password = StringHelper.GetPasswordFromAuthHeader(
                        context.HttpContext.Request
                    );

                    if (!string.IsNullOrEmpty(password))
                    {
                        var adminData = dataService.GetSecureData(AdminSecureData);

                        if (adminData != null)
                        {
                            isAdmin = SecurityHelper.PasswordCorrect(password, adminData.Value);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var logger = context.HttpContext.RequestServices.GetService<
                    ILogger<AdminAuthorise>
                >();
                logger?.LogError(e, "Admin authorisation check failed");
                isAdmin = false;
            }

            if (!isAdmin)
            {
                context.Result = new JsonResult(new ApiResponse(ok: false) { Error = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                //await base.OnActionExecutionAsync(context, next);
            }
            else
            {
                await next();
            }
        }

        #endregion Public Methods
    }
}
