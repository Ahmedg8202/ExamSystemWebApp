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
        Task<List<Student>> GetAllAsync();
    }
}
