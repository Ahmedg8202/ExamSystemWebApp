using AutoMapper;
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
        private readonly IMapper _mapper;

        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddSubjectAsync(Subjectdto subjectdto)
        {
            var subject = _mapper.Map<Subject>(subjectdto);

            await _unitOfWork.SubjectRepository.AddAsync(subject);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<Subject> GetSubjectByIdAsync(string subjectId)
        {
            return await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync(int page = 0, int pageSize = 0)
        {
            return await _unitOfWork.SubjectRepository.GetAllAsync(page, pageSize);
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

            await _unitOfWork.SubjectRepository.UpdateAsync(existingSubject);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<bool> DeleteSubjectAsync(string subjectId)
        {
            var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);
            if (subject == null)
                return false;

            await _unitOfWork.SubjectRepository.DeleteAsync(subject);
            return await _unitOfWork.CompleteAsync() > 0;
        }

        public async Task<IEnumerable<Subject>> GetAll(int page, int pageSize)
        {
            var subjects = await _unitOfWork.SubjectRepository.GetAllAsync(page, pageSize);
            return subjects;
        }
    }

}
