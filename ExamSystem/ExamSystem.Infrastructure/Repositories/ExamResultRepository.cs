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

        public async Task<bool> AddExamResult(ExamResult examResult)
        {
            await _context.AddAsync(examResult);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<ExamResult>> GetAll(string studentId)
        {
            return await _context.ExamResults.Where(er => er.StudentId == studentId).ToListAsync();
        }
    }
}
