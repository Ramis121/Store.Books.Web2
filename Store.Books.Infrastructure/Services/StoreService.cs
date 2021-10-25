using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Store.Books.Infrastructure.Services
{
    using Interfaces;
    using Store.Books.Domain;
    using Store.Books.Infrastructure.Data;
    using System.Threading.Tasks;

    public class StoreService : IStoreService
    {
        private readonly BookDbContext _context;
        private readonly ILogger _logger;
        public StoreService(BookDbContext context, ILogger<StoreService> logger)
        {
            _logger = logger;
            _context = context;
        }

        private async Task<Price> GetLastPrice(int bookId)
        {
            _logger.LogInformation($"GetLastPrice get price by bookId: {bookId}");
            var lastPrice = await _context.Prices.Include(p=>p.Book).Where(p => p.Book.Id == bookId)
                .OrderByDescending(p => p.Created).FirstOrDefaultAsync();
            if (!(lastPrice is null))
                _logger.LogInformation($"GetLastPrice price value: {lastPrice.Amount} by bookId: {bookId}");
            else _logger.LogWarning($"GetLastPrice price value not found by bookId: {bookId}");
            return lastPrice;
        }
        private async Task<(Price, Exception)> GetLastPriceEx(int bookId)
        {
            try
            {
                return (await GetLastPrice(bookId), null);
            }
            catch (Exception exception)
            {
                _logger.LogError($"GetLastPriceEx error: {exception.Message}");
                return (null, exception);
            }
        }
        public async Task<Book> GetBookById(int bookId)
        {
            try
            {
                var book = await _context.Books.FindAsync(bookId);
                return book;
            }
            catch (Exception exception)
            {
                _logger.LogError($"GetBookById error: {exception.Message}");
                return null;
            }
        }
        public async Task<Order> CreateOrUpdateOrder(int bookId, string userName)
        {
            var book = await GetBookById(bookId);
            if (book is null)
            {
                _logger.LogWarning($"CreateOrder: book not found for bookId: {bookId}");
                throw new NullReferenceException($"book not found for bookId: {bookId}");
            }
            var (lastPrice, _) = await GetLastPriceEx(bookId);
            if (lastPrice is null)
            {
                _logger.LogWarning($"CreateOrder: lastPrice not found for bookId: {bookId}");
                throw new NullReferenceException($"lastPrice not found for bookId: {bookId}");
            }
            if (lastPrice.Amount < 0)
            {
                _logger.LogWarning($"CreateOrder: price amount too low for bookId: {bookId}");
                throw new ArgumentOutOfRangeException($"lastPrice not found for bookId: {bookId}");
            }
            var order = await _context
                .Orders
                .Where(p => p.UserName == userName && p.Status == Domain.Enums.OrderStatusEnum.Created)
                .OrderByDescending(p => p.Created)
                .FirstOrDefaultAsync();
            if (order is null)
            {
                _logger.LogWarning($"CreateOrder: last order for user: {userName} not found, creating new.");
                var orderEntry = _context.Orders.Add(new Order
                {
                    UserName = userName,
                    Status = Domain.Enums.OrderStatusEnum.Created,
                    Created = DateTime.Now,
                    Total = lastPrice.Amount
                });
                await SafeSave();
                order = orderEntry.Entity;
            }
            return order;
        }
        public async Task<bool> AddToBusket(Order order, Domain.Book book, Price price)
        {
            var existing = await _context.Buskets
                .Include("Order")
                .Include("Book")
                .FirstOrDefaultAsync(p => p.Order.Id == order.Id && p.BookId== book.Id);
            if (!(existing is null))
            {
                _logger.LogWarning($"AddToBusket: bookId: {book.Id} exist in busket");
                return false;
            }
            _context.Buskets.Add(new Busket { Order = order, BookId = book.Id, PriceId = price.Id });
            return await SafeSave();
        }
        public bool DeleteFromBusket(Order order, int bookId)
        {
            //var busket = ????;
            return false;
        }
        public Order FindOrder(int orderId)
        {
            return _context.Orders.FirstOrDefault(p => p.Id == orderId);
        }
        public bool CompleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> SafeSave()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<Domain.Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }
        public IEnumerable<Domain.Book> FindBooks(string title)
        {
            return _context.Books.Where(p => p.Title.ToLower().Contains(title.ToLower()));
        }
        public Order GetLastUnpayedOrder(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }
    }
}