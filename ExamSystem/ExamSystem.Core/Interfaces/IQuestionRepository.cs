using ExamSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Core.Interfaces
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<List<Question>> GetAll(string subjectId);

        Task<string> GetCorrectAnswerId(string questionId);
        Task<Question> GetQuestionById(string questionId);
    }
}
