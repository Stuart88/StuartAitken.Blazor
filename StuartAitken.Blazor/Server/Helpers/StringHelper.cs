using System.Net.Http.Headers;
using System.Text;

namespace StuartAitken.Blazor.Server.Helpers
{
    public static class StringHelper
    {
        #region Public Methods

        public static string GetPasswordFromAuthHeader(HttpRequest request)
        {
            Microsoft.Extensions.Primitives.StringValues authHeader = request.Headers[
                "Authorization"
            ];

            if (authHeader.Any())
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (
                    authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase)
                    && authHeaderVal.Parameter != null
                )
                {
                    // decodedHeader == [username, password]

                    string[] decodedHeader = Encoding.UTF8
                        .GetString(Convert.FromBase64String(authHeaderVal.Parameter))
                        .Split(':');

                    if (decodedHeader.Length == 2)
                        return decodedHeader[1];
                }
            }

            return "";
        }

        #endregion Public Methods
    }
}
