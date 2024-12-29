using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IAuthService _authService;
        public StudentController(IStudentService studentService, IAuthService authService)
        {
            _studentService = studentService;
            _authService = authService;

        }

        [HttpGet("students")]
        public async Task<IActionResult> GetAllStudents([FromQuery] int page = 0, [FromQuery] int pageSize = 0) {
            var students = await _studentService.GetAll(page, pageSize);
            return Ok(students);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterStudentdto studentdto)
        {
            var result = await _authService.Register(studentdto);
            if (result.Result != "Success")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogindto logindto)
        {
            var result = await _authService.Login(logindto);
            if (result.Result != "Success")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
