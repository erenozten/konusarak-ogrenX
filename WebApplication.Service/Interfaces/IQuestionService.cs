using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Service.DTOs;

namespace WebApplication.Service.Interfaces
{
    public interface IQuestionService
    {
        Task<List<QuestionDto>> GetByQuizIdAsync(int quizId);
        Task SaveQuizAsync(List<QuestionDto> questionDtos, string articleTitle, string articleContent, string loggedInUserId);
    }
}
