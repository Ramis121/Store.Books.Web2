using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;
using Store.Books.Infrastructure.Data;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Infrastructure.Repos;
using Store.Product.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Product.Repos
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ProductDbContext context) : base(context) { }

        public async Task<Author> GetByTitle(string title)
        {
            var newContext = (ProductDbContext)context;
            var result = await newContext.Authors
                .Where(p => p.Title == title).ToListAsync();

            var result0 = from p in newContext.Authors
            where p.Title == title
            select p;

            return result0.FirstOrDefault();
        }
    }
}
