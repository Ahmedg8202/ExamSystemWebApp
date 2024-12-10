using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddSubjectAsync(Subjectdto subjectdto)
        {
            Subject subject = new Subject
            {
                SubjectId = Guid.NewGuid().ToString(),
                Name = subjectdto.Name,
                Description = subjectdto.Description,
                Duration = subjectdto.Duration,
                QuestionsNumber = subjectdto.QuestionsNumber,
                total = subjectdto.total
            };

            return await _unitOfWork.SubjectRepository.AddAsync(subject);
        }

        public async Task<Subject> GetSubjectByIdAsync(string subjectId)
        {
            return await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _unitOfWork.SubjectRepository.GetAllAsync();
        }

        public async Task<bool> UpdateSubjectAsync(string subjectId, Subjectdto subjectdto)
        {
            var existingSubject = await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);
            if (existingSubject == null)
                return false;

            existingSubject.Name = subjectdto.Name;
            existingSubject.Description = subjectdto.Description;
            existingSubject.Duration = subjectdto.Duration;
            existingSubject.QuestionsNumber = subjectdto.QuestionsNumber;
            existingSubject.total = subjectdto.total;

            return await _unitOfWork.SubjectRepository.UpdateAsync(existingSubject);
        }

        public async Task<bool> DeleteSubjectAsync(string subjectId)
        {
            var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);
            if (subject == null)
                return false;

            return await _unitOfWork.SubjectRepository.DeleteAsync(subject);
        }
    }

}
