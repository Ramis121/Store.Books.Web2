using Store.Books.Infrastructure.Interfaces;
using Store.Books.Infrastructure.Repos;
using Store.Books.Order.Data;

namespace Store.Books.Orders.Repos
{
    public class BusketRepository : GenericRepository<Domain.Busket>, IBusketRepository
    {
        public BusketRepository(OrderDbContext context) : base(context) { }
    }
}
