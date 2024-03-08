using DWShop.Application.Features.Catalog.Commands.Createlist;
using FluentValidation;

namespace DWShop.Application.Validations.Catalog.Commands.CreateList
{
    public class CreateCatalogListCommandValidator :
        AbstractValidator<CreateCatalogListCommand>
    {
        public CreateCatalogListCommandValidator()
        {
            RuleFor(x => x.CreateCatalogs).NotEmpty()
                .WithMessage("no debe ser vacio")
                .Must(HaveNoDuplicates)
                .WithMessage("No puede haber valores duplicados");
        }


        private bool HaveNoDuplicates<T>(List<T> catalogs)
        {
            var uniquevalues = new HashSet<T>(catalogs);

            return uniquevalues.Count == catalogs.Count;

        }
    }
}
