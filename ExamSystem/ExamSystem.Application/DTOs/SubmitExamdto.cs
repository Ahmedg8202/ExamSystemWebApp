using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTOs
{
    public class SubmitExamdto
    {
        public string SubjectId { get; set; }
        public string StudentId { get; set; }
        public string ExamId { get; set; }
        public List<question> questions { get; set; }
    }
    public class question
    {
        public string QuestionId { get; set; }
        public string AnswerId { get; set; }
    }

}
