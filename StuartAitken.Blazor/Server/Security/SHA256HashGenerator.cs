using System.Security.Cryptography;
using System.Text;

namespace StuartAitken.Blazor.Server.Security
{
    public class SHA256HashGenerator
    {
        #region Public Methods

        public static string GenerateHash(string inputData)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(inputData));
            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        #endregion Public Methods
    }
}
