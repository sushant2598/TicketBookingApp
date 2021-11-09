using System.Text.RegularExpressions;

namespace UserService.Shared
{
    public class PasswordPolicy
    {
        public static bool Valid(string password)
        {
            if (password.Length > 8)
            {
                if (Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[0-9]") && Regex.IsMatch(password, "[^A-Za-z0-9]"))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}