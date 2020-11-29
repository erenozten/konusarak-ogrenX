using System.Collections.Generic;

namespace WebApplication.Core.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}