using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Web.Models;
using System.Diagnostics;

namespace Store.Books.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthorRepository _authorRepository;
        public HomeController(
            IAuthorRepository authorRepository,
            ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
