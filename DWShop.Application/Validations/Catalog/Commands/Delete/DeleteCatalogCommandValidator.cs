using DWShop.Application.Features.Catalog.Commands.Delete;
using FluentValidation;

namespace DWShop.Application.Validations.Catalog.Commands.Delete
{
    public class DeleteCatalogCommandValidator : AbstractValidator<DeleteCatalogCommand>
    {
        public DeleteCatalogCommandValidator()
        {
            RuleFor(x=> x.Id)
                .NotEmpty()
                .GreaterThan(0)
                .NotNull();
        }
    }
}
