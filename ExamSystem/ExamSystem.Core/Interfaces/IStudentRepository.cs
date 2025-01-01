using ExamSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Core.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetByIdAsync(string studentId);
        Task<ApplicationUser> GetApplicationUserById(string studentId);
        Task<List<Student>> GetAllAsync(int page = 0, int pageSize = 0);
        void UpdateStudentStatus(ApplicationUser student);
    }
}
