using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.DTOs
{
    public class Dashboarddto
    {
        public int StudentNumber { get; set; }//studentService
        public int ExamCompleted { get; set; }//examservice
        public int PassedExams { get; set; }
        public int  FailedExam { get; set; }
    }
}
