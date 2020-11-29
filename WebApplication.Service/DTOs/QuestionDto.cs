using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Service.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        
        [Required]
        public string Text { get; set; }
        public List<AnswerDto> AnswerDtos { get; set; }
    }
}
