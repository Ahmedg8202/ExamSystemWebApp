using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTOs
{
    public class Subjectdto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuestionsNumber { get; set; }
        public int Duration { get; set; }
        public int total { get; set; }
    }
}
