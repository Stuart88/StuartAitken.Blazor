using StuartAitken.Blazor.Shared.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace StuartAitken.Blazor.Client.Helpers
{
    public static class HttpHelpers
    {
        #region Private Fields

        private static readonly JsonSerializerOptions jsonSerializerOptions =
            new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
            };

        #endregion Private Fields

        #region Public Methods

        public static void AddAuthHeader(this HttpClient http, string pass)
        {
            if (string.IsNullOrEmpty(pass))
            {
                http.DefaultRequestHeaders.Authorization = null;
            }
            else
            {
                string headerVal = Convert.ToBase64String(Encoding.UTF8.GetBytes($"user:{pass}"));
                http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", headerVal);
            }
        }

        public static async Task CustomDeleteAsync(this HttpClient http, string uri)
        {
            try
            {
                var response = await http.DeleteAsync(uri);

                await HandleApiResponse(response);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<TResponse> CustomPostAsJsonAsync<TRequest, TResponse>(
            this HttpClient http,
            string uri,
            TRequest data
        ) where TResponse : new()
        {
            try
            {
                var response = await http.PostAsJsonAsync(uri, data);

                return await GetResponseData<TResponse>(response);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<TResponse> CustomPutAsJsonAsync<TRequest, TResponse>(
            this HttpClient http,
            string uri,
            TRequest data
        ) where TResponse : new()
        {
            try
            {
                var response = await http.PutAsJsonAsync(uri, data);

                return await GetResponseData<TResponse>(response);
            }
            catch
            {
                throw;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static async Task<T> GetResponseData<T>(HttpResponseMessage response)
            where T : new()
        {
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Server Error: {response.StatusCode}");

            string content = await response.Content.ReadAsStringAsync();

            ApiResponse<T>? resp = JsonSerializer.Deserialize<ApiResponse<T>>(
                content,
                jsonSerializerOptions
            );

            if (resp == null)
            {
                throw new Exception("Failed to deserialize response!");
            }

            if (!resp.Ok)
            {
                throw new Exception(resp.Error);
            }

            if (resp.Data == null)
            {
                throw new Exception("Response data was null!");
            }

            return resp.Data;
        }

        /// <summary>
        /// Processes Http response and throws an exception on any problems
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static async Task HandleApiResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Server Error: {response.StatusCode}");

            string content = await response.Content.ReadAsStringAsync();

            ApiResponse? resp = JsonSerializer.Deserialize<ApiResponse>(
                content,
                jsonSerializerOptions
            );

            if (resp == null)
            {
                throw new Exception("Failed to deserialize response!");
            }

            if (!resp.Ok)
            {
                throw new Exception(resp.Error);
            }
        }

        #endregion Private Methods
    }
}
