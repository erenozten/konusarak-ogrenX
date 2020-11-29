using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.Core.Entities;
using WebApplication.Core.Repositories;

namespace WebApplication.Data.Repository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ApplicationContext _context;

        public QuizRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Task<List<Quiz>> GetAll()
        {
            return _context.Quizzes.ToListAsync();
        }
    }
}
