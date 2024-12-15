using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTOs
{
    public class Questiondto
    {
        public string SubjectId { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public string CorrecAnswer { get; set; }
    }
}
