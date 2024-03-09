using AutoMapper;
using DWShop.Application.Responses.Catalog;
using DWShop.Infrastructure.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Catalog.Queries
{
    public class GetCatalogQueryHandler : IRequestHandler<GetCatalogQuery,
        IResult<IEnumerable<CatalogResponse>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Catalog, int> repository;
        private readonly IMapper mapper;

        public GetCatalogQueryHandler(IRepositoryAsync<Domain.Entities.Catalog,int> 
            repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IResult<IEnumerable<CatalogResponse>>> Handle(GetCatalogQuery request, 
            CancellationToken cancellationToken)
        {
            var products = await repository.GetAllAsync();

          //  products = new List<Domain.Entities.Catalog> {  products.First() };

            var productResponse = mapper.Map<List<CatalogResponse>>(products);

            return await Result<List<CatalogResponse>>.SuccessAsync(productResponse);
        }
    }
}
