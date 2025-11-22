using Microsoft.EntityFrameworkCore;
using Mooditor.Api.Data;
using Mooditor.Api.DTOs;
using Mooditor.Api.Models;

namespace Mooditor.Api.Services
{
    public class MoodEntryService : IMoodEntryService
    {
        private readonly AppDbContext _context;

        public MoodEntryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MoodEntry>> GetAllAsync(Guid? userId = null)
        {
            IQueryable<MoodEntry> q = _context.MoodEntries.AsNoTracking();
            if (userId.HasValue)
                q = q.Where(m => m.UserId == userId.Value);

            return await q.OrderByDescending(m => m.CreatedAt).ToListAsync();
        }

        public async Task<MoodEntry?> GetByIdAsync(Guid id)
        {
            return await _context.MoodEntries.FindAsync(id);
        }

        public async Task<MoodEntry> CreateAsync(CreateMoodEntryDto dto, Guid userId)
        {
            var todayStart = DateTime.UtcNow.Date;
            var todayEnd = todayStart.AddDays(1);

            var exists = await _context.MoodEntries.AnyAsync(m =>
                m.UserId == userId && m.CreatedAt >= todayStart && m.CreatedAt < todayEnd);

            if (exists)
            {}

            var entry = new MoodEntry
            {
                UserId = userId,
                Score = dto.Score,
                Note = dto.Note,
                CreatedAt = DateTime.UtcNow
            };

            _context.MoodEntries.Add(entry);
            await _context.SaveChangesAsync();
            return entry;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateMoodEntryDto dto)
        {
            var entry = await _context.MoodEntries.FindAsync(id);
            if (entry == null) return false;

            if (dto.Score.HasValue) entry.Score = dto.Score.Value;
            if (dto.Note is not null) entry.Note = dto.Note;

            _context.MoodEntries.Update(entry);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entry = await _context.MoodEntries.FindAsync(id);
            if (entry == null) return false;

            _context.MoodEntries.Remove(entry);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
