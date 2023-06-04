using DevAssignment.CommonLayer.Core.ResponseModels;
using DevAssignment.CommonLayer.Dtos;
using DevAssignment.RepositoryLayer.Repositories;
using DevAssignment.ServiceLayer.Services;
using EntityLayer.DbContext.Entities;
using FluentValidation;
using System.Net;
using System.Text.RegularExpressions;

namespace DevAssignment.ServiceLayer.Implementations
{
    public class UserService : IUserService
    {
        private readonly IValidator<RegisterUserDto> _validator;
        private readonly IUserRepository _UserRepository;
        private const string EMAIL_PATTERN = @"^[\w]{1,}[\w.+-]{0,}@[\w-]{2,}([.][a-zA-Z]{2,}|[.][\w-]{2,}[.][a-zA-Z]{2,})$";

        public UserService(IValidator<RegisterUserDto> validator, IUserRepository UserRepository)
        {
            _validator = validator;
            _UserRepository = UserRepository;
        }
        public async Task<Response> RegisterUser(RegisterUserDto registerUser)
        {
            var validations = _validator.Validate(registerUser);
            if (!validations.IsValid)
            {
               return new Response(HttpStatusCode.BadRequest, validations.Errors.Select(x => new ValidationFailureDto
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage,
                    ErrorCode = x.ErrorCode
                }).ToList());
            }

            if(await IsUserExistAsync(registerUser.Email))
            {
                return new Response(HttpStatusCode.Conflict, "Email already exists. Please choose a different email!.");
            }

            var User = new User()
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Email = registerUser.Email,
                UserName = registerUser.Email
            };

            var registerUserResponse = await _UserRepository.RegisterUser(User, registerUser.Password);

            if (!registerUserResponse)
                return new Response(HttpStatusCode.BadRequest, "User registration failed. Please try again.");

            return new Response(HttpStatusCode.Created, "The user has been registered successfully.");
        }

        public async Task<bool> IsUserExistAsync(string email)
        {
            var IsUserExist = await _UserRepository.IsExistingUser(email);

            return IsUserExist? true :  false;
        }
    }


}
