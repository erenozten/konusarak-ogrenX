﻿using System.Collections.Generic;

namespace WebApplication.Core.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Text { get; set; }
        public Quiz Quiz { get; set; }
        public List<Answer> Answers { get; set; }
    }
}