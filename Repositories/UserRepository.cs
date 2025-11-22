using Microsoft.EntityFrameworkCore;
using Mooditor.Api.Data;
using Mooditor.Api.Models;

namespace Mooditor.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) => _db = db;

        public async Task<User?> GetByEmailAsync(string email) =>
            await _db.Users.SingleOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByIdAsync(Guid id) =>
            await _db.Users.Include(u => u.MoodEntries).SingleOrDefaultAsync(u => u.Id == id);

        public async Task<User> AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
