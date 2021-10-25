using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Store.Books.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBus _bus;
        public HomeController(IBus bus)
        {
            _bus = bus;
        }

        public async Task<IActionResult> Index()
        {
            await _bus.PubSub.PublishAsync<MqMessage>(new MqMessage
            {
                From = "Me",
                To = "You",
                Title = "Hello",
                Body = "Магжан реальный гей",
                Created = System.DateTime.Now
            });
            return View();
        }
        
    }
}
