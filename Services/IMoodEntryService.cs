using Mooditor.Api.DTOs;
using Mooditor.Api.Models;

namespace Mooditor.Api.Services
{
    public interface IMoodEntryService
    {
        Task<MoodEntry?> GetByIdAsync(Guid id);
        Task<IEnumerable<MoodEntry>> GetAllAsync(Guid? userId = null);
        Task<MoodEntry> CreateAsync(CreateMoodEntryDto dto, Guid userId);
        Task<bool> UpdateAsync(Guid id, UpdateMoodEntryDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
