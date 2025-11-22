using Microsoft.EntityFrameworkCore;
using Mooditor.Api.Data;
using Mooditor.Api.Models;

namespace Mooditor.Api.Repositories
{
    public class MoodEntryRepository : IMoodEntryRepository
    {
        private readonly AppDbContext _db;
        public MoodEntryRepository(AppDbContext db) => _db = db;

        public async Task<MoodEntry> AddAsync(MoodEntry entry)
        {
            _db.MoodEntries.Add(entry);
            await _db.SaveChangesAsync();
            return entry;
        }

        public async Task<MoodEntry?> GetByIdAsync(Guid id) =>
            await _db.MoodEntries.FindAsync(id);

        public async Task<IEnumerable<MoodEntry>> GetByUserIdAsync(Guid userId) =>
            await _db.MoodEntries
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();

        public async Task UpdateAsync(MoodEntry entry)
        {
            _db.MoodEntries.Update(entry);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(MoodEntry entry)
        {
            _db.MoodEntries.Remove(entry);
            await _db.SaveChangesAsync();
        }
    }
}
