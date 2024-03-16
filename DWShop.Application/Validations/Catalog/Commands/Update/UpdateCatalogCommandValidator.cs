

using DWShop.Application.Features.Catalog.Commands.Update;
using FluentValidation;

namespace DWShop.Application.Validations.Catalog.Commands.Update
{
    public class UpdateCatalogCommandValidator : AbstractValidator<UpdateCatalogCommand>
    {
        public UpdateCatalogCommandValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x=> x.Name)
                .NotEmpty().WithMessage("El nombre no puede ser vacio!!")
                .NotNull()
                .MinimumLength(2);
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Category).NotEmpty().NotNull();
        }
    }
}
