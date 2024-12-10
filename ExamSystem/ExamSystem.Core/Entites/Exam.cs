using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Core.Entites
{
    public class Exam
    {
        public string ExamId { get; set; }
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
        public List<ExamQuestion> ExamQuestions { get; set; }
    }
}
