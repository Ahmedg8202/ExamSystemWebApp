using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {

        private readonly IExamService _examService;
        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet("exam/History")]
        public async Task<IActionResult> AllExamHistory()
        {
            var examHistory = await _examService.AllExamResults();

            if (examHistory == null || !examHistory.Any())
            {
                return BadRequest("No exam history found");
            }

            return Ok(examHistory);
        }

        [HttpGet("exam/History/{studentId}")]
        public async Task<IActionResult> ExamHistory(string studentId)
        {
            var examHistory = await _examService.GetExamHistoryForStudent(studentId);

            if (examHistory == null || !examHistory.Any())
            {
                return BadRequest("No exam history found for this student.");
            }

            return Ok(examHistory);
        }

        [HttpGet("random")]
        public async Task<ActionResult<Exam>> GetRandomExam()
        {
            var exam = await _examService.GetRandomExam();
            if (exam == null)
            {
                return NotFound("No exams found.");
            }
            return Ok(exam);
        }

        [HttpGet("exam/{examId}")]
        public async Task<ActionResult<Exam>> GetExam(string examId)
        {
            var exam = await _examService.ExamById(examId);
            if (exam == null)
            {
                return NotFound("No exams found.");
            }
            return Ok(exam);
        }

        [HttpPost("exam/submit")]
        public async Task<ActionResult<ExamResult>> SubmitExam([FromBody] SubmitExamdto exam)
        {
            var result = await _examService.SubmitExam(exam);
            if (result == null)
            {
                return BadRequest("Exam submission failed.");
            }

            return Ok(result);
        }

        [HttpPost("exam/add")]
        public async Task<ActionResult> AddExam([FromForm] Examdto examdto)
        {
            var success = await _examService.AddExam(examdto);
            if (!success)
            {
                return BadRequest("Failed to add exam.");
            }

            return Ok();
        }

        [HttpPut("exam/update")]
        public async Task<ActionResult> UpdateExam([FromBody] Examdto examdto)
        {
            var success = await _examService.UpdateExam(examdto);
            if (!success)
            {
                return BadRequest("Failed to update exam.");
            }

            return Ok();
        }

        [HttpDelete("exam/delete/{examId}")]
        public async Task<ActionResult> DeleteExam(string examId)
        {
            var success = await _examService.DeleteExam(examId);
            if (!success)
            {
                return NotFound($"Exam with ID {examId} not found.");
            }

            return Ok();
        }
    }
}