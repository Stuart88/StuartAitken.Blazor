using System.Security.Cryptography;
using System.Text;

namespace StuartAitken.Blazor.Server.Security
{
    public static class SecurityHelper
    {
        #region Public Methods

        public static bool PasswordCorrect(string userPass, string actualPassHash)
        {
            string userPassHash = SHA256HashGenerator.GenerateHash(userPass);

            // Constant-time comparison - a plain `==` on hashes is vulnerable to a timing attack,
            // since string equality short-circuits on the first mismatched character.
            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(userPassHash),
                Encoding.UTF8.GetBytes(actualPassHash)
            );
        }

        #endregion Public Methods
    }
}
