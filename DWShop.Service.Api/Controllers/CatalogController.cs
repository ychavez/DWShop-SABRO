using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Application.Features.Catalog.Commands.Createlist;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController: ControllerBase
    {
        private readonly IMediator mediator;

        public CatalogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct([FromBody]CreateCatalogCommand command)
            => Ok( await mediator.Send(command));

        [HttpPost("InsertList")]
        public async Task<ActionResult<IResult>> CreateProductList([FromBody] CreateCatalogListCommand command)
            => Ok(await mediator.Send(command));
    }
}
