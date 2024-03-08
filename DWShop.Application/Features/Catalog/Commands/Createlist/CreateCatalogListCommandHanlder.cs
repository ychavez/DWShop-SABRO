using DWShop.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Application.Features.Catalog.Commands.Createlist
{
    internal class CreateCatalogListCommandHanlder
        : IRequestHandler<CreateCatalogListCommand, IResult>
    {
        public async Task<IResult> Handle(CreateCatalogListCommand request, CancellationToken cancellationToken)
        {
            return Result.Success("");
        }
    }
}
