using ExamSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Core.Interfaces
{
    public interface IExamResultRepository: IGenericRepository<ExamResult>
    {
        Task<Dashboard> Dashboard();
    }
}
