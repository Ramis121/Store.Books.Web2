using Store.Books.Domain;
using System.Threading.Tasks;

namespace Store.Books.Infrastructure.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author> 
    {
        Task<Author> GetByTitle(string title);
    }
}
