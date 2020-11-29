using System.ComponentModel.DataAnnotations;

namespace WebApplication.Service.DTOs
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }

        [Required]
        public string Text { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}