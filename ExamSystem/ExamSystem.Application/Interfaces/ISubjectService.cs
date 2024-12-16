using ExamSystem.Application.DTOs;
using ExamSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAll(int page, int pageSize);
        Task<bool> AddSubjectAsync(Subjectdto subjectdto);
        Task<Subject> GetSubjectByIdAsync(string subjectId);
        Task<IEnumerable<Subject>> GetAllSubjectsAsync(int page, int pageSize);
        Task<bool> UpdateSubjectAsync(string subjectId, Subjectdto subjectdto);
        Task<bool> DeleteSubjectAsync(string subjectId);
    }
}
