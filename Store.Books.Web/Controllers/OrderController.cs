using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Web.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Books.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreService _service;
        public OrderController(
            IStoreService service,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            _service = service;
        }
        public async Task<ActionResult> Buy(int bookId)
        {
            var book = await _service.GetBookById(bookId);
            return View();
        }
        public async Task<ActionResult> Bu1y(int bookId)
        {
            var book = await _service.GetBookById(bookId);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Buy(int bookId, string userName)
        {
            var order = await _service.CreateOrUpdateOrder(bookId, userName);
            return View(order);
        }

    }
}
