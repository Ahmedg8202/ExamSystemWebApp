using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using ExamSystem.Application.Services;
using ExamSystem.Core.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService service)
        {
            _questionService = service;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("questions/{subjectId}")]
        public async Task<ActionResult> AllQuestions(string subjectId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var exams = await _questionService.GetAllQuestions(subjectId, page, pageSize);
            if (exams == null)
            {
                return BadRequest("No Questions found.");
            }
            return Ok(exams);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("question/{questionId}")]
        public async Task<ActionResult> GetQuestionById(string questionId)
        {
            var exam = await _questionService.GetQuestionById(questionId);
            if (exam == null)
            {
                return BadRequest("No Questions found.");
            }
            return Ok(exam);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("question")]
        public async Task<IActionResult> Question([FromBody]Questiondto questiondto)
        {
            var result = await _questionService.AddQuestion(questiondto);
            if (!result)
                return BadRequest();

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("question")]
        public async Task<IActionResult> UpdateQuestion(Questiondto questiondto)
        {
            var result = await _questionService.UpdateQuestion(questiondto);
            if (!result)
                return BadRequest();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("question/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(string questionId)
        {
            var result = await _questionService.DeleteQuestion(questionId);
            if (!result)
                return BadRequest();

            return Ok();
        }
    }
}
