
using DWShop.Application.Features.Catalog.Commands.Update;
using DWShop.Shared.Wrapper;

namespace DWShop.Client.Infrastructure.Managers.Products.Update
{
    public interface IUpdateProductManager : IManager
    {
        Task<IResult> UpdateProduct(UpdateCatalogCommand command);
    }
}
