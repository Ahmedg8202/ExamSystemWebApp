using ExamSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTOs
{
    public class QuestionExamdto
    {
        public string Id { get; set; }
        public List<ExamQuestion> ExamQuestions { get; set; }
    }
}
