using Store.Books.Infrastructure.Interfaces;
using Store.Books.Infrastructure.Repos;
using Store.Books.Order.Data;

namespace Store.Books.Orders.Repos
{
    public class OrderRepository : GenericRepository<Domain.Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext context) : base(context) { }
    }
}
