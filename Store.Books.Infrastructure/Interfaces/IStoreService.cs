using Store.Books.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Books.Infrastructure.Interfaces
{
    public interface IStoreService
    {
        Task<Domain.Book> GetBookById(int bookId);
        Task<Domain.Order> CreateOrUpdateOrder(int bookId, string userName);
        Task<bool> AddToBusket(Domain.Order order, Domain.Book book, Price price);
        bool DeleteFromBusket(Domain.Order order, int bookId);
        bool CompleteOrder(int orderId);
        Domain.Order FindOrder(int orderId);
        IEnumerable<Domain.Book> FindBooks(string title);
        //Order GetLastUnpayedOrder(string userName);
        Task<IEnumerable<Domain.Book>> GetBooks();
        Task<IEnumerable<Author>> GetAuthors();
    }
}