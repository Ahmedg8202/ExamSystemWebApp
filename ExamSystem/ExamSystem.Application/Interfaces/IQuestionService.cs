using ExamSystem.Application.DTOs;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllQuestions();
        Task<Question> GetQuestionById(string questionId);
        Task<bool> AddQuestion(Questiondto questiondto);
        Task<bool> UpdateQuestion(Questiondto questiondto);
        Task<bool> DeleteQuestion(string questionId);
    }
}
