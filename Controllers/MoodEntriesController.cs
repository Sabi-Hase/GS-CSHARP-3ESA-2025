using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mooditor.Api.DTOs;
using Mooditor.Api.Services;

namespace Mooditor.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/moodentries")]
    public class MoodEntriesController : ControllerBase
    {
        private readonly IMoodEntryService _service;

        public MoodEntriesController(IMoodEntryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = Guid.Parse(User.FindFirst("id")!.Value);
            var items = await _service.GetAllAsync(userId);

            var dtos = items.Select(i => new MoodEntryDto
            {
                Id = i.Id,
                Score = i.Score,
                Note = i.Note,
                CreatedAt = i.CreatedAt,
                UserId = i.UserId
            });

            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetMoodEntryById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entry = await _service.GetByIdAsync(id);
            if (entry == null) return NotFound();

            return Ok(new MoodEntryDto
            {
                Id = entry.Id,
                Score = entry.Score,
                Note = entry.Note,
                CreatedAt = entry.CreatedAt,
                UserId = entry.UserId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMoodEntryDto dto)
        {
            var userId = Guid.Parse(User.FindFirst("id")!.Value);
            var created = await _service.CreateAsync(dto, userId);

            return CreatedAtRoute("GetMoodEntryById",
                new { id = created.Id, version = "1.0" },
                new MoodEntryDto
                {
                    Id = created.Id,
                    Score = created.Score,
                    Note = created.Note,
                    CreatedAt = created.CreatedAt,
                    UserId = created.UserId
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMoodEntryDto dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
