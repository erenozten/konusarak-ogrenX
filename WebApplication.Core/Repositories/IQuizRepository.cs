using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Core.Entities;

namespace WebApplication.Core.Repositories
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetAll();
    }
}
