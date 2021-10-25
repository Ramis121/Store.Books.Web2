using FluentValidation;
using Store.Product.Api.CQRS.Comands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Product.Api.Validators
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty();
            RuleFor(c => c.Title).MinimumLength(3);
            RuleFor(c => c.Title).MaximumLength(500);
        }
    }
}
