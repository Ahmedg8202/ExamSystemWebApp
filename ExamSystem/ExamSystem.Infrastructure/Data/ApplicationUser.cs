using Microsoft.AspNetCore.Identity;

namespace ExamSystem.Infrastructure.Data
{
    public class ApplicationUser: IdentityUser
    {
        public bool Active { get; set; } = true;
    }
}
