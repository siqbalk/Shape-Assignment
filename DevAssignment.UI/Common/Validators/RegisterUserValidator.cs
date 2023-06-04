using System.Text.RegularExpressions;

namespace DevAssignment.UI.Common.Validators
{
    public static class RegisterUserValidator
    {
        public static IEnumerable<string> PasswordValidation(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                yield return "Password is required!";
                yield break;
            }
            if (password.Length < 8)
                yield return "Password must be at least of length 8";
            if (!Regex.IsMatch(password, @"[A-Z]"))
                yield return "Password must contain at least one capital letter";
            if (!Regex.IsMatch(password, @"[a-z]"))
                yield return "Password must contain at least one lowercase letter";
            if (!Regex.IsMatch(password, @"[0-9]"))
                yield return "Password must contain at least one digit";
            if (!Regex.IsMatch(password, @"[^A-Za-z0-9]"))
                yield return "Password must contain at least one special character";
        }

        public static IEnumerable<string> FirstNameValidation(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                yield return "First Name is required!";
                yield break;
            }
            if (firstName.Length > 12)
                yield return "First name cannot exceed 12 characters";
        }

        public static IEnumerable<string> LastNameValidation(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                yield return "First Name is required!";
                yield break;
            }
            if (lastName.Length > 16)
                yield return "Last name cannot exceed 16 characters";
        }
    }
}
