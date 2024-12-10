using ExamSystem.Application.DTOs;
using ExamSystem.Core.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Interfaces
{
    public interface IAdminService
    {
        Task<Dashboarddto> Dashboard();
        List<IdentityUser> AllStudent();
        List<ExamResult> studentExams(string studentId);
        //Task EnableStudentAsync(Guid studentId, bool isEnabled);
    }
}
