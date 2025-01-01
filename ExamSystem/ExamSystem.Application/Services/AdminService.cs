using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using ExamSystem.Application.Validators;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Dashboard> Dashboard()
        {
            return await _unitOfWork.ExamResultRepository.Dashboard();
        }

        public async Task<bool> EnableStudentAsync(string studentId, bool isEnabled)
        {
            var student = await _unitOfWork.StudentRepository.GetApplicationUserById(studentId);
            if (student == null)
                return false;

            student.Active = isEnabled;

            _unitOfWork.StudentRepository.UpdateStudentStatus(student);
            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}
