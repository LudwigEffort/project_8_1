using System.Text;

namespace LoginWebAPI.Helper
{
    public class AuthHelper
    {
        public string GenerateNuance(int passwordLength)
        {
            return (passwordLength * new Random().Next(1, 100)).ToString();
        }

        public bool ValidatePasswordWithNuance(string pwWithNuance, string originalPassword, string nuance)
        {
            var receivedPassword = pwWithNuance.Replace(nuance, "");
            return receivedPassword == originalPassword;
        }

        public string GenerateToken(string email)
        {
            string randomString = GenerateRandomString(10);
            return email + "-" + randomString;
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }

    }
}