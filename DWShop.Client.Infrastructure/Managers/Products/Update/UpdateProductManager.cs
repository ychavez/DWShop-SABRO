using DWShop.Application.Features.Catalog.Commands.Update;
using DWShop.Client.Infrastructure.Extensions;
using DWShop.Shared.Wrapper;
using System.Net.Http.Json;


namespace DWShop.Client.Infrastructure.Managers.Products.Update
{
    public class UpdateProductManager : IUpdateProductManager
    {
        private readonly HttpClient httpClient;

        public UpdateProductManager(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IResult> UpdateProduct(UpdateCatalogCommand command)
        {
            var response = await httpClient
                .PutAsJsonAsync("/api/catalog", command);

            return await response.ToResult();
        }
    }
}
