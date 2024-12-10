using ExamSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Userdto> Register(RegisterStudentdto registerdto);
        Task<Userdto> Login(UserLogindto logindto);
    }
}
