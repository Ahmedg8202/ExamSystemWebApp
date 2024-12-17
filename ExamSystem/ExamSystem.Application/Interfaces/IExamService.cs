using ExamSystem.Application.DTOs;
using ExamSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Interfaces
{
    public interface IExamService
    {
        Task<IEnumerable<ExamResultdto>> GetExamHistoryForStudent(string studentId);
        Task<IEnumerable<ExamResultdto>> AllExamResults(int page, int pageSize);
        Task<IEnumerable<ExamFromdb>> AllExams();
        Task<ExamQuestiondto> GetRandomExam(string subjectId);
        Task<ExamQuestiondto> ExamById(string examId);
        Task<ExamResult> SubmitExam(SubmitExamdto exam);
        Task<bool> AddExam(Examdto examdto);
        Task<bool> UpdateExam(Examdto examdto);
        Task<bool> DeleteExam(string examId);
    }
}
