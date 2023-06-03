using DevAssignment.CommonLayer.Dtos;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace DevAssignment.CommonLayer.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        private readonly IConfiguration _configuration;
        private const string MOBILE_DEFAULT_PATTERN = @"^(?:\+92|0)[1-9]\d{9}$";
        private const string EMAIL_PATTERN = @"^[\w]{1,}[\w.+-]{0,}@[\w-]{2,}([.][a-zA-Z]{2,}|[.][\w-]{2,}[.][a-zA-Z]{2,})$";

        public RegisterUserValidator(IConfiguration configuration)
        {
            _configuration = configuration;

            RuleFor(x => x.FirstName)
               .NotEmpty()
               .WithMessage("First name cannot be empty")
               .MaximumLength(12)
               .WithMessage("First name cannot exceed 12 characters");

            RuleFor(x => x.LastName)
               .NotEmpty()
               .WithMessage("Last name cannot be empty")
               .MaximumLength(16)
               .WithMessage("Last name cannot exceed 16 characters");


            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .Matches(EMAIL_PATTERN)
                .WithMessage("Email must be valid.");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password cannot be empty")
               .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
               .Must(password => string.IsNullOrEmpty(password) || Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).*$"))
               .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password cannot be empty")
                .MinimumLength(8)
                .WithMessage("Confirm Password must be at least 8 characters long")
                .Equal(c => c.Password)
                .WithMessage("PASSWORD_MATCH_FAIL");
        }
    }
}
