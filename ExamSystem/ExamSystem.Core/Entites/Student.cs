﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Core.Entites
{
    public class Student
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public bool active { get; set; } = true;
        public string Token { get; set; }
        public string Result { get; set; }
    }
}
