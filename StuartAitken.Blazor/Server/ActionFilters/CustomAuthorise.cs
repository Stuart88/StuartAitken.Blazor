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
            var dataService = context.HttpContext.RequestServices.GetService<SecureDataService>();
            var controller = context.Controller as CustomControllerBase;

            bool isAdmin = false;

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

            if (!isAdmin)
            {
                context.Result = new JsonResult(new ApiResponse("Authorisation failed!"));
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
