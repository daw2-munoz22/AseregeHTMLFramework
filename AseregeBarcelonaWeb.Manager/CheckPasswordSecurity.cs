using AseregeBarcelonaWeb.Manager.Enums;

namespace AseregeBarcelonaWeb.Manager
{
    public class PasswordSecurity
    {
        public static PasswordSecurityLevel Check(string password)
        {
            if (password.Length < 8)
            {
                return PasswordSecurityLevel.Weak;
            }

            bool hasLowerCase = false;
            bool hasUpperCase = false;
            bool hasDigit = false;
            bool hasSymbol = false;

            foreach (char c in password)
            {
                if (char.IsLower(c))
                {
                    hasLowerCase = true;
                }
                else if (char.IsUpper(c))
                {
                    hasUpperCase = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else
                {
                    hasSymbol = true;
                }
            }

            if (hasLowerCase && hasUpperCase && hasDigit && hasSymbol)
            {
                return PasswordSecurityLevel.VeryStrong;
            }
            else if ((hasLowerCase && hasUpperCase && hasDigit) || (hasLowerCase && hasUpperCase && hasSymbol) || (hasLowerCase && hasDigit && hasSymbol) || (hasUpperCase && hasDigit && hasSymbol))
            {
                return PasswordSecurityLevel.Strong;
            }
            else if ((hasLowerCase && hasUpperCase) || (hasLowerCase && hasDigit) || (hasLowerCase && hasSymbol) || (hasUpperCase && hasDigit) || (hasUpperCase && hasSymbol) || (hasDigit && hasSymbol))
            {
                return PasswordSecurityLevel.Medium;
            }
            else
            {
                return PasswordSecurityLevel.Weak;
            }
        }
    }
}
