using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Books.Domain.DTO;
using Store.Books.Order.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Books.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IStoreService _service;
        public OrdersController(IStoreService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateOrder order)
        {
            var dbOrder = await _service.CreateOrUpdateOrder(order.BookId, order.Title, order.PriceId, order.Price, order.UserName);
            return Ok(dbOrder);
        }
    }
}
