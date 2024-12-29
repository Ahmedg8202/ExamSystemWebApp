using ExamSystem.Application.Services;
using ExamSystem.Core.Entites;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Repositories
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        private readonly ApplicationDBContext _context;

        public ExamRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<ExamQuestion>> GetExamById(string examId)
        {
            return _context.ExamQuestions
                .Include(e => e.Question)
                    .ThenInclude(q => q.Answers)
                .Include(e => e.Exam)  
                    .ThenInclude(ex => ex.Subject)
                .Where(e => e.ExamId == examId)
                .ToListAsync();
        }

        public async Task<List<ExamQuestion>> GetRandomExamAsync(string subjectId)
        {
            var randomExamId = await _context.Exams
                .Where(e => e.SubjectId == subjectId)
                .OrderBy(e => Guid.NewGuid())
                .Select(e => e.ExamId)
                .FirstOrDefaultAsync();

            if (randomExamId == null)
            {
                return null;
            }

            var examQuestions = await _context.ExamQuestions
                .Include(e => e.Exam.Subject)
                .Include(e => e.Question)
                .ThenInclude(e => e.Answers)
                .Where(e => e.ExamId == randomExamId)
                .ToListAsync();

            return examQuestions;
        }

        public async Task<List<Exam>> GetExamsBySubjectAsync(string subjectId)
        {
            return await _context.Exams.Where(e => e.SubjectId == subjectId).ToListAsync();
        }

        public Task<ExamResult> GetExamResultAsync(string studentId, string examId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<ExamResult>> GetExamHistoryAsync(string studentId, int page, int pageSize)
        {
            return await _context.ExamResults
                .Include(er => er.Exam)
                .ThenInclude(ex => ex.Subject)
                .Where(er => er.StudentId == studentId)
                .OrderByDescending(er => er.DateTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


    }

}
