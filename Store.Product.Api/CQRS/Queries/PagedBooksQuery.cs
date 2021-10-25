using MediatR;
using Store.Books.Domain;
using Store.Books.Infrastructure.Interfaces;
using Store.Product.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Product.Api.CQRS.Queries
{
    public class PagedBooksQuery : IRequest<IEnumerable<Book>>, ICacheableQuery
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public bool BypassCache => false;
        public string CacheKey => $"book-{Page}-{PerPage}";

        public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(10);
    }
    public class PagedBooksQueryHadler : IRequestHandler<PagedBooksQuery, IEnumerable<Book>>
    {
        private readonly IBookRepository _repository;
        public PagedBooksQueryHadler(IBookRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Book>> Handle(PagedBooksQuery command, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_repository.GetPaged(command.Page, command.PerPage));
        }
    }
}
