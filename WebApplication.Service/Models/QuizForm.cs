using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Core.Entities;
using WebApplication.Service.DTOs;

namespace WebApplication.Service.Models
{
    // QuizViewModel
    public class QuizForm
    {
        public QuizForm()
        {
            QuestionDtos = new List<QuestionDto>();
        }

        public List<QuestionDto> QuestionDtos { get; set; }
    }
}
