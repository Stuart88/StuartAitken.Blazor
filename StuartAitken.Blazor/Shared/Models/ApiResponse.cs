namespace StuartAitken.Blazor.Shared.Models
{
    public class ApiResponse
    {
        #region Public Properties

        public string Error { get; set; } = "";

        public bool Ok { get; set; } = true;

        #endregion Public Properties

        #region Public Constructors

        public ApiResponse() { }

        public ApiResponse(bool ok)
        {
            this.Ok = ok;
        }

        public ApiResponse(Exception e)
        {
            this.Ok = false;
            string error = e.Message;
            if(e.InnerException != null && !string.IsNullOrEmpty(e.InnerException.Message))
            {
                error += $"\n{e.InnerException.Message}";

            }
            this.Error = error;
        }

        #endregion Public Constructors
    }

    public class ApiResponse<T> : ApiResponse where T : new()
    {
        #region Public Properties

        public T? Data { get; set; } = default;

        #endregion Public Properties

        #region Public Constructors

        public ApiResponse() { }

        public ApiResponse(T data)
        {
            this.Data = data;
            this.Ok = true;
        }

        public ApiResponse(bool ok) : base(ok) { }

        public ApiResponse(Exception e) : base(e) { }

        #endregion Public Constructors
    }
}
