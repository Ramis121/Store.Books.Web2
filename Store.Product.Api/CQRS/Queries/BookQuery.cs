using MediatR;
using Store.Books.Domain;
using Store.Books.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Product.Api.CQRS.Queries
{
    public class BookQuery : IRequest<IEnumerable<Book>>
    {
        public string Title { get; set; }
    }
    public class BookQueryHandler : IRequestHandler<BookQuery, IEnumerable<Book>>
    {
        private readonly IBookRepository _repository;

        public BookQueryHandler(IBookRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Book>> Handle(BookQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(p => p.Title == request.Title);
        }
    }
}
