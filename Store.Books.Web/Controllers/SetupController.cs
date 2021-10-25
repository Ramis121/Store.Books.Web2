using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Books.Domain;
using Store.Books.Infrastructure.Data;
using System;
using System.Linq;

namespace Store.Books.Web.Controllers
{
    public class SetupController : Controller
    {
        private readonly BookDbContext _context;
        private readonly ILogger<SetupController> _logger;
        public SetupController(
            ILogger<SetupController> logger,
            BookDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        // GET: Setup
        public ActionResult Index()
        {
            ViewBag.IsEmptyAuthors = !_context.Authors.Any();
            ViewBag.IsEmptyGenres = !_context.Genres.Any();
            ViewBag.IsEmptyBooks = !_context.Books.Any();
            ViewBag.IsEmptyPrices = !_context.Prices.Any();
            return View();
        }
        [HttpPost]
        public ActionResult Index(string create_authors, string create_genres, string create_books, string create_prices)
        {
            if (!string.IsNullOrWhiteSpace(create_authors))
            {
                _context.Authors.Add(new Author { Title = "Тестовый автор 1" });
                _context.Authors.Add(new Author { Title = "Тестовый автор 2" });
                _context.Authors.Add(new Author { Title = "Тестовый автор 3" });
                _logger.LogInformation("added 3 authors");
            }
            if (!string.IsNullOrWhiteSpace(create_genres))
            {
                _context.Genres.Add(new Genre { Title = "Тестовый жанр 1" });
                _context.Genres.Add(new Genre { Title = "Тестовый жанр 2" });
                _context.Genres.Add(new Genre { Title = "Тестовый жанр 3" });
                _logger.LogInformation("added 3 genres");
            }
            if (!string.IsNullOrWhiteSpace(create_books))
            {
                _context.Books.Add(new Book { Title = "Тестовая книга 1" });
                _context.Books.Add(new Book { Title = "Тестовая книга 2" });
                _context.Books.Add(new Book { Title = "Тестовая книга 3" });
                _logger.LogInformation("added 3 books");
            }
            var ok = SafeSave();
            if (ok != "ok")
                ViewBag.Error = ok;
            else
            {
                if (!string.IsNullOrWhiteSpace(create_prices))
                {
                    var book1 = _context.Books.FirstOrDefault(p => p.Title == "Тестовая книга 1");
                    var book2 = _context.Books.FirstOrDefault(p => p.Title == "Тестовая книга 2");
                    _context.Prices.Add(new Price { Book = book1, Amount = 100, Created = DateTime.Now });
                    _context.Prices.Add(new Price { Book = book2, Amount = 100, Created = DateTime.Now.AddMonths(-1) });
                    _context.Prices.Add(new Price { Book = book2, Amount = 150, Created = DateTime.Now });
                    _logger.LogInformation("added 3 prices");
                }
                SafeSave();
            }
            ViewBag.IsEmptyAuthors = !_context.Authors.Any();
            ViewBag.IsEmptyGenres = !_context.Genres.Any();
            ViewBag.IsEmptyBooks = !_context.Books.Any();
            ViewBag.IsEmptyPrices = !_context.Prices.Any();
            return View();
        }
        private string SafeSave()
        {
            try
            {
                _context.SaveChanges();
                return "ok";
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return exception.Message;
            }

        }
    }
}