using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Core.Entites
{
    public class ExamResult
    {
        public string ExamResultId { get; set; }
        public string StudentId { get; set; }
        public IdentityUser? Student { get; set; }
        public string ExamId { get; set; }
        public Exam? Exam { get; set; }
        public string SubjectId { get; set; }
        public DateTime DateTime { get; set; }
        public int Score { get; set; }
        public bool Status { get; set; }
    }
}
