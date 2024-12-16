using ExamSystem.Core.Entites;
using ExamSystem.Core.Interfaces;
using ExamSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public StudentRepository(ApplicationDBContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;

        }

        public async Task<List<Student>> GetAllAsync(int page, int pageSize)
        {
            var role = await _roleManager.FindByNameAsync("Student");

            if (role == null)
            {
                return new List<Student>();
            }

            var userIds = await _context.UserRoles
                .Where(ur => ur.RoleId == role.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();
            var users = new List<IdentityUser> { };

            if (page <= 0 || pageSize <= 0)
            {
                users = await _context.Users
                   .Where(u => userIds.Contains(u.Id))
                   .ToListAsync();
            }
            else
            {
                users = await _context.Users
                    .Where(u => userIds.Contains(u.Id))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return users.Select(u => new Student
            {
                id = u.Id,
                userName = u.UserName,
                email = u.Email,
                Result = "",
                Token = ""
            }).ToList();
        }


        public async Task<Student> GetByIdAsync(string studentId)
        {
            var role = await _roleManager.FindByNameAsync("Student");

            if (role == null)
                return null;

            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.RoleId == role.Id && ur.UserId == studentId);

            if (userRole == null)
                return null;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == studentId);

            if (user == null)
                return null;

            return new Student
            {
                id = user.Id,
                userName = user.UserName,
                email = user.Email
            };
        }
    }
}
