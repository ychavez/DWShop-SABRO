using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Application.Features.Catalog.Commands.Createlist;
using DWShop.Application.Features.Catalog.Commands.Delete;
using DWShop.Application.Features.Catalog.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{

    public class CatalogController : BaseApiController
    {

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct([FromBody] CreateCatalogCommand command)
            => Ok(await mediator.Send(command));

        [HttpPost("InsertList")]
        public async Task<ActionResult<IResult>> CreateProductList([FromBody] CreateCatalogListCommand command)
            => Ok(await mediator.Send(command));

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
            => Ok(await mediator.Send(new DeleteCatalogCommand { Id = id }));

        [HttpGet]
        public async Task<ActionResult> GetCatalogs()
            => Ok(await mediator.Send(new GetCatalogQuery()));
    }
}
