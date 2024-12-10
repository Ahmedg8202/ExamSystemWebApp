using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IStudentService _studentService;
        public AdminController(IAdminService adminService, IStudentService studentService)
        {
            _adminService = adminService;
            _studentService = studentService;

        }

        [HttpGet("Dashboard")]
        public async Task<ActionResult> Dashboard() {
            var result = await _adminService.Dashboard();
            return Ok(result);
        }

        [HttpPost("Access")]
        public IActionResult StudentStatus()
        {
            return Ok();
        }

    }
}
