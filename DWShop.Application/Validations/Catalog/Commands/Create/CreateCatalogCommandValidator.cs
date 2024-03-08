using DWShop.Application.Features.Catalog.Commands.Create;
using FluentValidation;

namespace DWShop.Application.Validations.Catalog.Commands.Create
{
    public class CreateCatalogCommandValidator:
        AbstractValidator<CreateCatalogCommand>
    {
        public CreateCatalogCommandValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre no puede ser vacio porfavor");
        }
    }
}
