using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Store.Books.Domain;
using Store.Books.Domain.Enums;
using System.Threading.Tasks;
using Store.Books.Infrastructure.Interfaces;


namespace Store.Books.Order.Services
{
    
    public class StoreService : Interfaces.IStoreService
    {
        private readonly IOrderRepository _orders;
        private readonly IPaymentRepository _payments;
        private readonly IBusketRepository _buskets;
        private readonly ILogger<StoreService> _logger;
        public StoreService(IOrderRepository orders,
            IPaymentRepository payments,
            IBusketRepository buskets,
            ILogger<StoreService> logger)
        {
            _orders = orders;
            _payments = payments;
            _buskets = buskets;
            _logger = logger;
        }

        public async Task<Domain.Order> CreateOrUpdateOrder(int bookId, string title, int priceId, decimal price, string userName)
        {
            if (0 >= bookId)
            {
                _logger.LogWarning($"CreateOrder: bookId <= 0: {bookId}");
                throw new ArgumentOutOfRangeException($"bookId <= 0: {bookId}");
            }
            if (price < 0)
            {
                _logger.LogWarning($"CreateOrder: price amount too low for bookId: {bookId}");
                throw new ArgumentOutOfRangeException($"lastPrice not found for bookId: {bookId}");
            }
            var order = await GetLastUnpayedOrder(userName);
            if (order is null)
            {
                _logger.LogWarning($"CreateOrder: last order for user: {userName} not found, creating new.");
                await _orders.Insert(new Domain.Order
                {
                    UserName = userName,
                    Status = OrderStatusEnum.Created,
                    Created = DateTime.Now,
                    Total = price
                });
                order = await GetLastUnpayedOrder(userName);
            }
            await AddToBusket(order, bookId, title, priceId, price);
            return order;
        }
        public async Task<bool> AddToBusket(Domain.Order order, int bookId, string title, int priceId, decimal price)
        {
            var existing = _buskets
                .Get(p => p.Order.Id == order.Id && p.BookId == bookId, includeProperties: "Order, Book");
            if (!(existing is null))
            {
                _logger.LogWarning($"AddToBusket: bookId: {bookId} exist in busket");
                return false;
            }
            await _buskets.Insert(new Busket { Order = order, BookId = bookId, PriceId=priceId, Price = price });
            return SafeSave();
        }
        public bool DeleteFromBusket(Domain.Order order, int bookId)
        {
            //var busket = ????;
            return false;
        }
        public async Task<Domain.Order> FindOrder(int orderId)
        {
            return await _orders.GetById(orderId);
        }
        public bool CompleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }
        public bool SafeSave()
        {
            try
            {
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task<Domain.Order> GetLastUnpayedOrder(string userName)
        {
            return (await _orders.Get(p => p.UserName == userName && p.Status == OrderStatusEnum.Created))
                .OrderByDescending(p => p.Created)
                .FirstOrDefault();
        }
    }
}
