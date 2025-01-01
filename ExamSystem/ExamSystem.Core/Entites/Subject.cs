using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Core.Entites
{
    public class Subject
    {
        public string SubjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuestionsNumber { get; set; }
        public int Duration { get; set; } //min
        public int total { get; set; }
    }
}
