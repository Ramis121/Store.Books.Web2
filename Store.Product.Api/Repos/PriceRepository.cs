using Store.Books.Domain;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Infrastructure.Repos;
using Store.Product.Data;

namespace Store.Product.Repos
{
    public class PriceRepository : GenericRepository<Price>, IPriceRepository
    {
        public PriceRepository(ProductDbContext context) : base(context) { }
    }
}
