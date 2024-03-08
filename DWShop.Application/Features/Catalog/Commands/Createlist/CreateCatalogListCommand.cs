using DWShop.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWShop.Application.Features.Catalog.Commands.Createlist
{
    public class CreateCatalogListCommand : IRequest<IResult>
    {
        public List<CreateCatalog> CreateCatalogs { get; set; }
    }


    public class CreateCatalog
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public decimal Price { get; set; }
        public string? PhotoURL { get; set; }
    }
}
