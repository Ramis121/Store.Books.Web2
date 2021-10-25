using MediatR;
using Store.Books.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Product.Api.CQRS.Comands
{
    public class CreateBookCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public int Year { get; set; }
    }
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, bool>
    {
        private readonly IBookRepository _repository;
        public CreateBookCommandHandler(IBookRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            await _repository.Insert(new Books.Domain.Book
            {
                Title = command.Title,
                Year = command.Year
            });
            return true;
        }
    }
}
