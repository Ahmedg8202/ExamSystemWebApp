using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Core.Entites
{
    public class Dashboard
    {
        public int Students { get; set; }
        public int CompletedExams { get; set; }
        public int PassedExams { get; set;}
        public int FailedExams { get; set;}
    }
}
