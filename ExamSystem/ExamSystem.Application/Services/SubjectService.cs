using AutoMapper;
using ExamSystem.Application.DTOs;
using ExamSystem.Application.Interfaces;
using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<IEnumerable<Subject>> GetAll(int page, int pageSize)
        {
            var subjects = await _unitOfWork.SubjectRepository.GetAllAsync(page, pageSize);
            return subjects;
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

        public async Task<bool> UpdateSubjectAsync(string subjectId, Subjectdto subjectdto)
        {
            var existingSubject = await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);
            if (existingSubject == null)
                return false;

            if (!subjectdto.Name.IsNullOrEmpty()) existingSubject.Name = subjectdto.Name;
            if (!subjectdto.Description.IsNullOrEmpty()) existingSubject.Description = subjectdto.Description;
            if (subjectdto.Duration > 0) existingSubject.Duration = subjectdto.Duration;
            if (subjectdto.QuestionsNumber > 0) existingSubject.QuestionsNumber = subjectdto.QuestionsNumber;
            if (subjectdto.total > 0) existingSubject.total = subjectdto.total;

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

    }

}
