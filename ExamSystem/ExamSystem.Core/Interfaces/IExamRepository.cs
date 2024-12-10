﻿using ExamSystem.Core.Entites;
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
        Task AddQuestionAsync(Question question);
        Task<ExamQuestion> GetRandomExamAsync();
        Task<List<ExamQuestion>> GetExamById(string examId);
        Task<List<Exam>> GetExamsBySubjectAsync(string subject);
        Task AddExamResultAsync(ExamResult result);
        Task<ExamResult> GetExamResultAsync(string studentId, string examId);
        Task<IList<ExamResult>> GetExamHistoryAsync(string studentId);

    }
}