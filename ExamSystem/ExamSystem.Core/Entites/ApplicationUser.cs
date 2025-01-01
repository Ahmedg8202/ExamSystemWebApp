using Microsoft.AspNetCore.Identity;

namespace ExamSystem.Core.Entites
{
    public class ApplicationUser: IdentityUser
    {
        public bool Active { get; set; } = true;
    }
}
