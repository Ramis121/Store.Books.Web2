using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Books.Infrastructure.Interfaces;
using Store.Books.Infrastructure.Repos;
using Store.Product.Api.CQRS.Comands;
using Store.Product.Api.CQRS.Queries;
using Store.Product.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Product.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator,
            IBookRepository bookRepository,
            ILogger<ProductController> logger)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string title)
        {
            return Ok(await _mediator.Send(new BookQuery { Title = title }));
        }

        [HttpGet("byId")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _bookRepository.GetById(id));
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(int page, int perPage)
        {
            return Ok(await _mediator.Send(new PagedBooksQuery { Page = page, PerPage = perPage }));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }
    }
}
