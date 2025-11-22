using Mooditor.Api.Models;

namespace Mooditor.Api.Repositories
{
    public interface IMoodEntryRepository
    {
        Task<MoodEntry> AddAsync(MoodEntry entry);
        Task<MoodEntry?> GetByIdAsync(Guid id);
        Task<IEnumerable<MoodEntry>> GetByUserIdAsync(Guid userId);
        Task UpdateAsync(MoodEntry entry);
        Task DeleteAsync(MoodEntry entry);
    }
}
