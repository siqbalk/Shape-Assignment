using EntityLayer.DbContext.Entities;

namespace DevAssignment.RepositoryLayer.Repositories
{
    public interface IUserRepository 
    {
        Task<bool> RegisterUser(User model, string password);
    }
}
