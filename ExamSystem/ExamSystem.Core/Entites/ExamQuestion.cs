using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Core.Entites
{
    public class ExamQuestion
    {
        public int Id { get; set; }
        public string ExamId { get; set; }
        public Exam Exam { get; set; }
        public string QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
