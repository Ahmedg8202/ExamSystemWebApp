using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Repositories
{
    public class ExamResultRepository: GenericRepository<ExamResult>, IExamResultRepository
    {
        private readonly ApplicationDBContext _context;
        public ExamResultRepository(ApplicationDBContext context):base(context)
        {
            _context = context;
        }

        public async Task<Dashboard> Dashboard()
        {
            var passed = await _context.ExamResults.CountAsync(e => e.Status);
            var failed = await _context.ExamResults.Where(er => er.Status == false).CountAsync();

            var studentRoleId = await _context.Roles
                .Where(r => r.Name == "Student")
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            int students = 0;

            if (!string.IsNullOrEmpty(studentRoleId))
                students = await _context.UserRoles
                    .Where(ur => ur.RoleId == studentRoleId)
                    .CountAsync();

            return new Dashboard
            {
                Students = students,
                CompletedExams = passed + failed,
                PassedExams = passed,
                FailedExams = failed
            };
        }
    }
}
