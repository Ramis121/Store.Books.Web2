using Microsoft.EntityFrameworkCore;
using Store.Books.Domain;
using Store.Books.Infrastructure.Data;
using Store.Books.Infrastructure.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Books.Infrastructure.Repos
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(BookDbContext context) : base(context) { }
    }
}
