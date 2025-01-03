﻿using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Repositories
{
    public class AdminRepository: IAdminRepository
    {
        private readonly ApplicationDBContext _context;
        public AdminRepository(ApplicationDBContext context) {
            _context = context;
        }

        public bool SubjectExists(string name)
        {
            return _context.Subjects.Any(s => s.Name == name);
        }
    }
}
