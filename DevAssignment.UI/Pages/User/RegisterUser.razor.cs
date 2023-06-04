using DevAssignment.UI.Models.User;
using MudBlazor;
using System.Net;
using System.Text.RegularExpressions;

namespace DevAssignment.UI.Pages.User;

public partial class RegisterUser
{

    private bool _loaded;
    bool success;
    string[] errors = { };
    MudTextField<string> password;
    MudForm form;
    public RegisterUserModel UserModel { get; set; } = new RegisterUserModel();
    private const string EMAIL_PATTERN = @"^[\w]{1,}[\w.+-]{0,}@[\w-]{2,}([.][a-zA-Z]{2,}|[.][\w-]{2,}[.][a-zA-Z]{2,})$";

    private IEnumerable<string> PasswordStrength(string password)
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

    private async Task<IEnumerable<string>> EmailValidation(string email)
    {
        var validationMessages = new List<string>();

        if (string.IsNullOrWhiteSpace(email))
        {
            validationMessages.Add("Email cannot be empty!");
        }
        else if (!Regex.IsMatch(email, EMAIL_PATTERN))
        {
            validationMessages.Add("The email address is invalid");
        }
        else
        {
            var res = await _clientService.IsEmailExistAsync(email).ConfigureAwait(false);
            if (!res.IsSucceeded)
            {
                validationMessages.Add("Email already exists. Please choose a different email!");
            }
        }

        return validationMessages;
    }

    private IEnumerable<string> FirstNameValidation(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            yield return "First Name is required!";
            yield break;
        }
        if (firstName.Length > 12)
            yield return "First name cannot exceed 12 characters";
    }

    private IEnumerable<string> LastNameValidation(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            yield return "First Name is required!";
            yield break;
        }
        if (lastName.Length > 16)
            yield return "Last name cannot exceed 16 characters";
    }

    private string PasswordMatch(string confirmPassword)
    {

        if (password.Value != confirmPassword)
            return "The entered passwords do not match";
        return null;
    }

    [Obsolete]
    private async Task SubmitForm()
    {
        _loaded = true;
        _ = form.Validate();
        if (form.IsValid)
        {
            var res = await _clientService.RegisterUserAsync(UserModel).ConfigureAwait(false);

            if (!res.IsSucceeded)
            {
                _snackBar.Add("Something went wrong, Try again in a while", Severity.Error);
                return;
            }
            _loaded = true;
            _snackBar.Add("Document Type Successfully Added", Severity.Success);
             form.Reset();
            UserModel = new RegisterUserModel();
        }
    }

    protected override async Task OnInitializedAsync()
    {
       
        StateHasChanged();
    }
}
