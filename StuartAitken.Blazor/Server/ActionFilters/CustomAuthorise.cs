using System.Threading.RateLimiting;
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
                // Shared across every [AdminAuthorise]-protected endpoint, keyed by client IP,
                // using System.Threading.RateLimiting's own fixed-window limiter (registered in
                // Program.cs). Only a WRONG password consumes a permit here - a logged-in admin
                // resends the same correct password on every request, so normal back-to-back
                // admin activity (editing, uploading, reordering) never touches the budget; only
                // repeated wrong guesses do.
                var limiter = context.HttpContext.RequestServices
                    .GetService<PartitionedRateLimiter<string>>();
                string clientKey = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                using var lease = limiter?.AttemptAcquire(clientKey);

                if (lease != null && !lease.IsAcquired)
                {
                    context.Result = new JsonResult(
                        new ApiResponse(ok: false)
                        {
                            Error = "Too many attempts. Try again in a few minutes."
                        }
                    )
                    {
                        StatusCode = StatusCodes.Status429TooManyRequests
                    };
                }
                else
                {
                    context.Result = new JsonResult(new ApiResponse(ok: false) { Error = "Unauthorized" })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
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
