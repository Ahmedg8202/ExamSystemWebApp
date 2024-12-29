﻿using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using ExamSystem.Application.Validators;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ExamSystem.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<Dashboarddto> Dashboard()
        {
            var examResults = await _unitOfWork.ExamResultRepository.GetAllAsync();

            if (examResults == null || !examResults.Any())
                return null;

            var customExamResult = examResults.Select(result => new ExamResult
            {
                ExamId = result.ExamId,
                StudentId = result.StudentId,
                DateTime = result.DateTime,
                Score = result.Score,
                Status = result.Status
            }).ToList();

            var studentNumber = (await _unitOfWork.StudentRepository.GetAllAsync()).Count;
            var examCompleted = examResults.Count();
            var failedExams = examResults.Count(e => !e.Status);
            var passedExams = examResults.Count(e => e.Status);


            return new Dashboarddto {
                StudentNumber = studentNumber,
                ExamCompleted = examCompleted,
                FailedExam = failedExams,
                PassedExams = passedExams
            };
        }
    }
}
