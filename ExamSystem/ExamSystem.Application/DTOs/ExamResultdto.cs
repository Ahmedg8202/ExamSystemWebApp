using ExamSystem.Core.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTOs
{
    public class ExamResultdto
    {
        public string ExamResultId { get; set; }
        public string StudentId { get; set; }
        public string ExamId { get; set; }
        public string SubjectName{ get; set; }
        public string StudentName{ get; set; }
        public string SubjectId { get; set; }
        public DateTime DateTime { get; set; }
        public int Score { get; set; }
        public bool Status { get; set; }
    }
}
