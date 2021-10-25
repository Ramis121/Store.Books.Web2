using FluentValidation;
using Store.Product.Api.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Product.Api.Validators
{
    public class QueryValidator : AbstractValidator<PagedBooksQuery>
    {
        public QueryValidator()
        {
            RuleFor(c => c.PerPage).GreaterThan(0);
            RuleFor(c => c.Page).GreaterThan(0);
            RuleFor(c => c.PerPage).LessThan(21);
        }
    }
}
