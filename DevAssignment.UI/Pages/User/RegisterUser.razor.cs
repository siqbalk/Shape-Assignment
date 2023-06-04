using DevAssignment.UI.Common.Validators;
using DevAssignment.UI.Models.User;
using MudBlazor;
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
        return RegisterUserValidator.PasswordValidation(password);
    }

    private IEnumerable<string> FirstNameValidation(string firstName)
    {

        return RegisterUserValidator.FirstNameValidation(firstName);
    }

    private IEnumerable<string> LastNameValidation(string lastName)
    {
        return RegisterUserValidator.LastNameValidation(lastName);
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
            var isEmailExist = await _clientService.IsEmailExistAsync(email).ConfigureAwait(false);
            if (isEmailExist)
            {
                validationMessages.Add("Email already exists. Please choose a different email!");
            }
        }

        return validationMessages;
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
        _ = form.Validate();
        if (form.IsValid)
        {
            _loaded = true;
            var res = await _clientService.RegisterUserAsync(UserModel).ConfigureAwait(false);

            if (!res.IsSucceeded)
            {
                _snackBar.Add("Something went wrong, Try again in a while", Severity.Error);
                return;
            }
            _loaded = false;
            _snackBar.Add("The user has been registered successfully.", Severity.Success);
            form.Reset();
            UserModel = new RegisterUserModel();
        }
    }

    protected override async Task OnInitializedAsync()
    {

        StateHasChanged();
    }
}
