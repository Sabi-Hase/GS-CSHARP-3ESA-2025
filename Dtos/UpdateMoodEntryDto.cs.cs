using System.ComponentModel.DataAnnotations;

namespace Mooditor.Api.DTOs
{
    public class UpdateMoodEntryDto
    {
        [Range(1,5)]
        public int? Score { get; set; }

        [MaxLength(1000)]
        public string? Note { get; set; }
    }
}
