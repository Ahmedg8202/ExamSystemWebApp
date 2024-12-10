using ExamSystem.Application.Services;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using ExamSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        public IExamRepository ExamRepository { get; }
        public IQuestionRepository QuestionRepository { get; }
        public IExamResultRepository ExamResultRepository { get; }
        public IAdminRepository AdminRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public ISubjectRepository SubjectRepository { get; }
        public UnitOfWork(ApplicationDBContext context,
            IExamRepository examRepository, 
            IQuestionRepository questionRepository, 
            IAdminRepository adminRepository, 
            IStudentRepository studentRepository,
            IExamResultRepository examResultRepository,
            ISubjectRepository subjectRepository)
        {
            _context = context;
            ExamRepository = examRepository;
            QuestionRepository = questionRepository;
            AdminRepository = adminRepository;
            StudentRepository = studentRepository;
            ExamResultRepository = examResultRepository;
            SubjectRepository = subjectRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
