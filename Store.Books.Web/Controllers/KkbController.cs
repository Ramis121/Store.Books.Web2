using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Store.Books.Domain;
using Store.Books.Domain.Configs;
using Store.Books.Domain.DAO.Kkb;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Web.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Books.Web.Controllers
{
    public class KkbController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreService _service;
        private readonly KkbConfig _config;
        private readonly IKkbProtocolService _kkbService;
        public KkbController(
            IStoreService service,
            IOptions<KkbConfig> config,
            IKkbProtocolService kkbService,
            ILogger<HomeController> logger)
        {
            _kkbService = kkbService;
            _config = config?.Value;
            _logger = logger;
            _service = service;
        }
        // GET: KkbController
        [HttpPost]
        public async Task<ActionResult> Buy(int orderid)
        {
            
            var payment = _service.FindOrder(orderid);
            var sign = _kkbService.Build64Sync(payment.Id.ToString(), payment.Total);
            ViewBag.config = _config;
            return View(new KkbRequest
            {
                Email = payment.UserName,
                Sign = sign,
            });
            
            
        }
    }
}
