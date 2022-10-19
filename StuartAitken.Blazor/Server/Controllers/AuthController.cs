using Microsoft.AspNetCore.Mvc;
using StuartAitken.Blazor.Server.ActionFilters;
using StuartAitken.Blazor.Shared.Models;

namespace StuartAitken.Blazor.Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : CustomControllerBase
    {
        #region Public Methods

        [AdminAuthorise]
        [HttpGet]
        public ApiResponse AuthHeaderCheck()
        {
            // If user makes it to here, [AdminAuthorise] attribute passed so just send back 'Ok' ApiResonse

            return new ApiResponse(ok: true);
        }

        #endregion Public Methods
    }
}
