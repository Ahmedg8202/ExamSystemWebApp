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
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        private readonly ApplicationDBContext _context;
        public QuestionRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> GetCorrectAnswerId(string questionId)
        {
            return await _context.Questions
                .Where(q => q.QuestionId == questionId)
                .SelectMany(q => q.Answers)
                .Where(a => a.IsCorrect)
                .Select(a => a.AnswerId)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Question>> GetAll(string subjectId, int page, int pageSize)
        {
            return await _context.Questions
                .OrderDescending()
                .Where(q => q.SubjectId == subjectId)
                .Include(q => q.Answers)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<Question> GetQuestionById(string questionId)
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuestionId == questionId);
        }
    }
}
