using ExamSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTOs
{
    public class RequestedExamdto
    {
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }

        public List<Question> Questions { get; set; }
    }
}
