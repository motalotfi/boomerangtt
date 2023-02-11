using Boomrang.Service.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace Boomrang.Service.Implementations
{
    public class ComputeHash : IComputeHash
    {
        public string ComputeSha256Hash(string password)
        {
            // Create a SHA256   
            using SHA256 sha256Hash = SHA256.Create();

            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert byte array to a string   
            StringBuilder builder = new();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
