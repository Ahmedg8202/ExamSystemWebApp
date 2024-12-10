
using ExamSystem.Application.Services;
using ExamSystem.Core.Entites;

namespace ExamSystem.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IExamRepository ExamRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        IExamResultRepository ExamResultRepository { get; }
        IAdminRepository AdminRepository { get; }
        IStudentRepository StudentRepository { get; }
        Task<int> CompleteAsync();
    }
}
