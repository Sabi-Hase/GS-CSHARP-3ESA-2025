namespace Mooditor.Api.DTOs
{
    public class MoodEntryDto
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
    }
}
