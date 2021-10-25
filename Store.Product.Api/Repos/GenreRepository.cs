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
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ProductDbContext context) : base(context) { }
    }
}
