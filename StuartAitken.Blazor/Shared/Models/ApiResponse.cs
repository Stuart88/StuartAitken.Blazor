namespace StuartAitken.Blazor.Shared.Models
{
    public class ApiResponse
    {
        #region Public Properties

        public string Error { get; set; } = "";

        public bool Ok { get; set; } = true;

        #endregion Public Properties

        #region Public Constructors

        public ApiResponse()
        {
        }

        public ApiResponse(bool ok)
        {
            this.Ok = ok;
        }

        public ApiResponse(string errorMessage)
        {
            this.Ok = false;
            this.Error = errorMessage;
        }

        #endregion Public Constructors
    }

    public class ApiResponse<T> : ApiResponse where T : new()
    {
        #region Public Properties

        public T? Data { get; set; } = default;

        #endregion Public Properties

        #region Public Constructors
        public ApiResponse()
        {

        }

        public ApiResponse(T data)
        {
            this.Data = data;
            this.Ok = true;
        }

        public ApiResponse(bool ok) : base(ok)
        {
        }

        public ApiResponse(string errorMessage) : base(errorMessage)
        {
        }

        #endregion Public Constructors
    }
}