using Mooditor.Api.Models;

namespace Mooditor.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task<User> AddAsync(User user);
        Task SaveChangesAsync();
    }
}
