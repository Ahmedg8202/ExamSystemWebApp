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
                .ThenInclude(e => e.Answers)
                .Where(e => e.ExamId == examId).ToListAsync();
        }

        public async Task<List<ExamQuestion>> GetRandomExamAsync()
        {
            //int count = await _context.ExamQuestions
            //    .Select(e => e.ExamId)
            //    .Distinct()
            //    .CountAsync();

            //Random random = new Random();
            //int skip = random.Next(0, count);

            //return await _context.ExamQuestions
            //    .Include(e => e.Question)
            //    .ThenInclude(e => e.Answers)
            //    .Skip(skip)
            //    .Take(1)
            //    .FirstOrDefaultAsync();
            var randomExamId = await _context.Exams
                .OrderBy(e => Guid.NewGuid())  // Randomly orders exams
                .Select(e => e.ExamId)
                .FirstOrDefaultAsync();

            if (randomExamId == null)
            {
                return null;  // No exams found
            }

            // Fetch the questions associated with the random exam
            var examQuestions = await _context.ExamQuestions
                .Include(e => e.Question)
                .ThenInclude(e => e.Answers)
                .Where(e => e.ExamId == randomExamId)
                .ToListAsync();

            return examQuestions;
        }

        public async Task AddExamResultAsync(ExamResult result)
        {
            _context.ExamResults.Add(result);
            await _context.SaveChangesAsync();
        }

        public Task<List<Exam>> GetExamsBySubjectAsync(string subject)
        {
            throw new NotImplementedException();
        }

        public Task AddQuestionAsync(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<ExamResult> GetExamResultAsync(string studentId, string examId)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<ExamResult>> GetExamHistoryAsync(string studentId)
        {
            return await _context.ExamResults.Where(er => er.StudentId == studentId).ToListAsync();
        }

    }

}
