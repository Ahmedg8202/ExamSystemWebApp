using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services
{
    public interface IExamRepository: IGenericRepository<Exam>
    {
        Task<List<ExamQuestion>> GetRandomExamAsync(string subjectId);
        Task<List<ExamQuestion>> GetExamById(string examId);
        Task<List<Exam>> GetExamsBySubjectAsync(string subjectId);
        Task<ExamResult> GetExamResultAsync(string studentId, string examId);
        Task<IList<ExamResult>> GetExamHistoryAsync(string studentId, int page, int pageSize);
       
    }
}
