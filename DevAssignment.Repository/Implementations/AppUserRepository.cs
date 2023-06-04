using DevAssignment.RepositoryLayer.Repositories;
using EntityLayer.DbContext.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace DevAssignment.RepositoryLayer.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<User> _userManager;

        public UserRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userManager = _serviceProvider.GetRequiredService<UserManager<User>>();

        }

        public async Task<bool> RegisterUser(User model, string password)
        {
            var result = await _userManager.CreateAsync(model, password);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsExistingUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return false;
            }
            return true;
        }
    }
}
