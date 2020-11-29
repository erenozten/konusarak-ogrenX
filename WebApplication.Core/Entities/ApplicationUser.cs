using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}