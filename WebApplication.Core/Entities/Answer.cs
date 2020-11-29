﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Core.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public Question Question { get; set; }
    }
}
