using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet("Dashboard")]
        public async Task<ActionResult> Dashboard() {
            var result = await _adminService.Dashboard();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("studentStatus/{studentId}")]
        public async Task<IActionResult> StudentStatus(string studentId, bool isEnabled)
        {
            var result = await _adminService.EnableStudentAsync(studentId, isEnabled);

            if (!result)
                return BadRequest();

            return Ok();
        }

    }
}
