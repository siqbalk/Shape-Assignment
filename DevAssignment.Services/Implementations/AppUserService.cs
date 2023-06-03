using DevAssignment.CommonLayer.Core.ResponseModels;
using DevAssignment.CommonLayer.Dtos;
using DevAssignment.RepositoryLayer.Repositories;
using DevAssignment.ServiceLayer.Services;
using EntityLayer.DbContext.Entities;
using FluentValidation;
using System.Net;

namespace DevAssignment.ServiceLayer.Implementations
{
    public class UserService : IUserService
    {
        private readonly IValidator<RegisterUserDto> _validator;
        private readonly IUserRepository _UserRepository;

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
    }


}
