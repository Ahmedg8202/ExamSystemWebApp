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

        [HttpGet("History")]
        public async Task<IActionResult> AllExamHistory()
        {
            var examHistory = await _examService.AllExamResults();

            if (examHistory == null || !examHistory.Any())
            {
                return BadRequest("No exam history found");
            }

            return Ok(examHistory);
        }
        
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

        [HttpGet("Exam/{examId}")]
        public async Task<ActionResult<Exam>> GetExam(string examId)
        {
            var exam = await _examService.ExamById(examId);
            if (exam == null)
            {
                return NotFound("No exams found.");
            }
            return Ok(exam);
        }

        [HttpPost("submit")]
        public async Task<ActionResult<ExamResult>> SubmitExam([FromBody] SubmitExamdto exam)
        {
            var result = await _examService.SubmitExam(exam);
            if (result == null)
            {
                return BadRequest("Exam submission failed.");
            }

            return Ok(result);
        }

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