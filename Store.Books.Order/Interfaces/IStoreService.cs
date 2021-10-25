using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Books.Order.Interfaces
{
    public interface IStoreService
    {
        Task<Domain.Order> CreateOrUpdateOrder(int bookId, string title, int priceId, decimal price, string userName);
        Task<bool> AddToBusket(Domain.Order order, int bookId, string title, int priceId, decimal price);
        bool DeleteFromBusket(Domain.Order order, int bookId);
        bool CompleteOrder(int orderId);
        Task<Domain.Order> FindOrder(int orderId);
        Task<Domain.Order> GetLastUnpayedOrder(string userName);
    }
}
