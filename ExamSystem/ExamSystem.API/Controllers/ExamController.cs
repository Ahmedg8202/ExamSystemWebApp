using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet("History")]
        public async Task<IActionResult> AllExamHistory([FromQuery] int page, [FromQuery] int pageSize)
        {
            var examHistory = await _examService.AllExamResults(page, pageSize);

            if (examHistory == null || !examHistory.Any())
            {
                return BadRequest("No exam history found");
            }

            return Ok(examHistory);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Exams")]
        public async Task<IActionResult> AllExams()
        {
            var exams = await _examService.AllExams();

            if (exams == null || !exams.Any())
            {
                return BadRequest("No exam history found");
            }

            return Ok(exams);
        }

        [HttpGet("History/{studentId}")]
        public async Task<IActionResult> ExamHistory(string studentId)
        {
            var examHistory = await _examService.GetExamHistoryForStudent(studentId);

            if (examHistory == null || !examHistory.Any())
            {
                return BadRequest("No exam history found for this student.");
            }

            return Ok(examHistory);
        }

        [Authorize(Roles ="Student")]
        [HttpGet("random/{subjectId}")]
        public async Task<ActionResult<Exam>> GetRandomExam(string subjectId)
        {
            var exam = await _examService.GetRandomExam(subjectId);
            if (exam == null)
            {
                return NotFound("No exams found.");
            }
            return Ok(exam);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Exam/{examId}")]
        public async Task<ActionResult<ExamQuestiondto>> GetExam(string examId)
        {
            var exam = await _examService.ExamById(examId);
            if (exam == null)
            {
                return NotFound("No exams found.");
            }
            return Ok(exam);
        }

        [Authorize(Roles ="Student")]
        [HttpPost("submit")]
        public async Task<ActionResult> SubmitExam([FromBody] SubmitExamdto exam)
        {
            var result = await _examService.SubmitExam(exam);
            if (result == null)
            {
                return BadRequest("Exam submission failed.");
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<ActionResult> AddExam([FromBody] Examdto examdto)
        {
            var success = await _examService.AddExam(examdto);
            if (!success)
            {
                return BadRequest("Failed to add exam.");
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateExam([FromBody] Examdto examdto)
        {
            var success = await _examService.UpdateExam(examdto);
            if (!success)
            {
                return BadRequest("Failed to update exam.");
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{examId}")]
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