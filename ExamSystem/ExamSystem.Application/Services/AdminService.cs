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
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AdminService(IAdminRepository adminRepository, IUnitOfWork unitOfWork)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;

        }
        public List<IdentityUser> AllStudent()
        {
            throw new NotImplementedException();
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
                Score = result.Score,
                Status = result.Status
            }).ToList();

            var studentNumber = (await _unitOfWork.StudentRepository.GetAllAsync()).Count;
            var examCompleted = examResults.Count();
            var failedExams = examResults.Where(e => e.Status == false).ToList().Count();
            var passedExams = examResults.Where(e => e.Status == true).ToList().Count();
            
            return new Dashboarddto {
                StudentNumber = studentNumber,
                ExamCompleted = examCompleted,
                FailedExam = failedExams,
                PassedExams = passedExams
            };
        }

        public List<ExamResult> studentExams(string studentId)
        {
            throw new NotImplementedException();
        }
    }
}