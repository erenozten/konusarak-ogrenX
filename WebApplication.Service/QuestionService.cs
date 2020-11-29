using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApplication.Core.Entities;
using WebApplication.Data;
using WebApplication.Service.DTOs;
using WebApplication.Service.Interfaces;

namespace WebApplication.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationContext _context;

        public QuestionService(ApplicationContext context)
        {
            _context = context;
        }

        public Task<List<QuestionDto>> GetByQuizIdAsync(int quizId)
        {
            throw new NotImplementedException();
        }


        public async Task SaveQuizAsync(List<QuestionDto> questionDtos, string articleTitle, string articleContent, string loggedInUserId)
        {
            var newQuiz = new Quiz
            {
                Title = articleTitle,
                Content = articleContent,
                ApplicationUserId = loggedInUserId, 
                Questions = new List<Question>()
            };

            await _context.Quizzes.AddAsync(newQuiz);
            await _context.SaveChangesAsync();

            foreach (QuestionDto questionDto in questionDtos)
            {
                var question = new Question
                {
                    Text = questionDto.Text, 
                    QuizId = newQuiz.Id,
                    Answers = new List<Answer>()
                };

                await _context.Questions.AddAsync(question);
                await _context.SaveChangesAsync();

                foreach (AnswerDto answerDto in questionDto.AnswerDtos)
                {
                    var answer = new Answer
                    {
                        IsCorrectAnswer = answerDto.IsCorrectAnswer,
                        QuestionId = question.Id,
                        Text = answerDto.Text
                    };

                    await _context.Answers.AddAsync(answer);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
