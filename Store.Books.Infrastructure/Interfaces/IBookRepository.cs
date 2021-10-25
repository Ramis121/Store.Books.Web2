using Store.Books.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Books.Infrastructure.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book> 
    {
        Task<Book> GetByTitle(string title);
        IEnumerable<Book> GetPaged(int page, int perPage);
    }
}
