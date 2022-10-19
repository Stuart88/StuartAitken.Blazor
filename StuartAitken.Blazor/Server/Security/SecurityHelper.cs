namespace StuartAitken.Blazor.Server.Security
{
    public static class SecurityHelper
    {
        #region Public Methods

        public static bool PasswordCorrect(string userPass, string actualPassHash)
        {
            return SHA256HashGenerator.GenerateHash(userPass) == actualPassHash;
        }

        #endregion Public Methods
    }
}
