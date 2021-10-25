using Store.Books.Infrastructure.Interfaces;
using Store.Product.Api.CQRS.Comands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Product.Api.Interfaces
{
    public interface ICreateBookRepository : IGenericRepository<CreateBookCommand>
    {
    }
}
