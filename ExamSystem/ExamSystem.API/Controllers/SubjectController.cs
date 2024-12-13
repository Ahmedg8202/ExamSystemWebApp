using Microsoft.AspNetCore.Mvc;
using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> AllSubjects()
        {
            var result = await _subjectService.GetAll();
            return Ok(result);
        }

        [HttpPost("Subject")]
        public async Task<IActionResult> AddSubject([FromBody] Subjectdto subjectdto)
        {
            var result = await _subjectService.AddSubjectAsync(subjectdto);
            if (!result)
            {
                return BadRequest("Failed to add the subject.");
            }

            return Ok("Subject added successfully.");
        }

        [HttpGet("Subject/{subjectId}")]
        public async Task<IActionResult> GetSubjectById(string subjectId)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(subjectId);
            if (subject == null)
            {
                return NotFound($"Subject with ID {subjectId} not found.");
            }

            return Ok(subject);
        }

        [HttpGet("Subjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(subjects);
        }

        [HttpPut("Subject/{subjectId}")]
        public async Task<IActionResult> UpdateSubject(string subjectId, [FromBody] Subjectdto subjectdto)
        {
            var result = await _subjectService.UpdateSubjectAsync(subjectId, subjectdto);
            if (!result)
            {
                return NotFound($"Subject with ID {subjectId} not found or failed to update.");
            }

            return Ok("Subject updated successfully.");
        }

        [HttpDelete("Subject/{subjectId}")]
        public async Task<IActionResult> DeleteSubject(string subjectId)
        {
            var result = await _subjectService.DeleteSubjectAsync(subjectId);
            if (!result)
            {
                return NotFound($"Subject with ID {subjectId} not found or failed to delete.");
            }

            return Ok("Subject deleted successfully.");
        }
    }
}
