using DevAssignment.CommonLayer.Core.ResponseModels;
using DevAssignment.CommonLayer.Dtos;
using DevAssignment.ServiceLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevAssignment.Api.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string REGISTER = "register";
        private readonly IUserService _UserService;

        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }

        [AllowAnonymous]
        [HttpPost(REGISTER)]
        public async Task<Response> Register(RegisterUserDto  registerUser)
        {
            return await _UserService.RegisterUser(registerUser);
        }

        [AllowAnonymous]
        [HttpGet("{email}/isEmailExist")]
        public async Task<bool> IsEmailExist([FromRoute] string email)
        {
            return await _UserService.IsUserExistAsync(email);
        }
    }
}
