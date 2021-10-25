using Store.Books.Infrastructure.Interfaces;
using Store.Books.Infrastructure.Repos;
using Store.Books.Order.Data;

namespace Store.Books.Orders.Repos
{
    public class PaymentRepository : GenericRepository<Domain.Payment>, IPaymentRepository
    {
        public PaymentRepository(OrderDbContext context) : base(context) { }
    }
}
