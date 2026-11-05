namespace StuartAitken.Blazor.Server.Exceptions
{
    /// <summary>
    /// Marks an exception whose Message is safe to return to API callers verbatim.
    /// Anything else (raw framework/DB exceptions) must be logged, never echoed to the client.
    /// </summary>
    public class UserFacingException : Exception
    {
        public UserFacingException(string message) : base(message) { }
    }
}
