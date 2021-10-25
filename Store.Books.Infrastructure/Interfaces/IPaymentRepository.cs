using Store.Books.Domain;
using System.Threading.Tasks;

namespace Store.Books.Infrastructure.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment> { }
}
