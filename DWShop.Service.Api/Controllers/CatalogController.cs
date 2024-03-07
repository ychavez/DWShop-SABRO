using DWShop.Application.Features.Catalog.Commands.Create;
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
    }
}
