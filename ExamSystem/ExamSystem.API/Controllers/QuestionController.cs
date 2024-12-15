using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using ExamSystem.Application.Services;
using ExamSystem.Core.Entites;
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

        [HttpGet("questions/{subjectId}")]
        public async Task<ActionResult> AllQuestions(string subjectId)
        {
            var exams = await _questionService.GetAllQuestions(subjectId);
            if (exams == null)
            {
                return BadRequest("No Questions found.");
            }
            return Ok(exams);
        }

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

        [HttpPost("question")]
        public async Task<IActionResult> Question([FromBody]Questiondto questiondto)
        {
            var result = await _questionService.AddQuestion(questiondto);
            if (!result)
                return BadRequest();

            return Ok(result);
        }

        [HttpPut("question")]
        public async Task<IActionResult> UpdateQuestion(Questiondto questiondto)
        {
            var result = await _questionService.UpdateQuestion(questiondto);
            if (!result)
                return BadRequest();

            return Ok();
        }

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
