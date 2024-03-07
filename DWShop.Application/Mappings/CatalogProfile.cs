using AutoMapper;
using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Domain.Entities;

namespace DWShop.Application.Mappings
{
    public class CatalogProfile: Profile
    {
        public CatalogProfile()
        {
            CreateMap<Catalog, CreateCatalogCommand>().ReverseMap();
        }
    }
}
