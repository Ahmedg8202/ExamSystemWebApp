using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Core.Entites
{
    public class Question
    {
        public string QuestionId { get; set; }
        public string Text { get; set; }
        public string SubjectId { get; set; }
        public List<Answer> Answers { get; set; }
        public List<ExamQuestion> ExamQuestions { get; set; }
    }
}
