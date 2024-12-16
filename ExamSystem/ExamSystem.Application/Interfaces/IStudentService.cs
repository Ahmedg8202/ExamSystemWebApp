using ExamSystem.Application.DTOs;
using ExamSystem.Core.Entites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAll(int page, int pageSize);

    }
}
