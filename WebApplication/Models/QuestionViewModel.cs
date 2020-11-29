using WebApplication.Service.DTOs;

namespace WebApplication.Models
{
    public class QuestionViewModel
    {
        public int CorrectAnswerIndex { get; set; }
        public QuestionDto Question { get; set; }
    }
}