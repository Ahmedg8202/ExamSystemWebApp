using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTOs
{
    public class ExamQuestiondto
    {
        public string ExamId { get; set; }
        public string SubjectId { get; set; }
        public List<QuestionExam> Questions { get; set; }
    }

    public class QuestionExam
    {
        public string questionId { get; set; }
        public string text { get; set; }
        public List<AnswerExam> Answers { get; set; }
    }
    public class AnswerExam
    {
        public string answerId { get; set; }
        public string text { get; set; }
        public bool isCorrect { get; set; }
    }
}
