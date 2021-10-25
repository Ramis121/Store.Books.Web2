using Store.Books.Infrastructure.Repos;
using Store.Product.Api.CQRS.Comands;
using Store.Product.Api.Interfaces;
using Store.Product.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Product.Api.Repos
{
    public class CreateBookRepository :GenericRepository<CreateBookCommand>, ICreateBookRepository
    {
        public CreateBookRepository(ProductDbContext context) : base(context) { }

    }
}
