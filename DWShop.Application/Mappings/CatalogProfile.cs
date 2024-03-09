using AutoMapper;
using DWShop.Application.Features.Catalog.Commands.Create;
using DWShop.Application.Features.Identity.Commands.Login;
using DWShop.Application.Responses.Catalog;
using DWShop.Domain.Entities;

namespace DWShop.Application.Mappings
{
    public class CatalogProfile: Profile
    {
        public CatalogProfile()
        {
          
            
            CreateMap<Catalog, CreateCatalogCommand>().ReverseMap();
            CreateMap<Catalog, CatalogResponse>().ReverseMap();
        }
    }
}
