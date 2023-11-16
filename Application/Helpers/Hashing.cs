using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers;

public class Hashing
{
    public static string HashString(string text, string salt = "")
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        using var sha = new SHA256Managed();
        var textBytes = Encoding.UTF8.GetBytes(text + salt);
        var hashBytes = sha.ComputeHash(textBytes);

        var hash = BitConverter
            .ToString(hashBytes)
            .Replace("-", string.Empty);

        return hash;
    }
}