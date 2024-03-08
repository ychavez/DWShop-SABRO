using AutoMapper;
using DWShop.Infrastructure.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;


namespace DWShop.Application.Features.Catalog.Commands.Create
{

    public class CreateCatalogCommandHandler :
        IRequestHandler<CreateCatalogCommand, IResult<int>>
    {
        private readonly IMapper mapper;
        private readonly IRepositoryAsync<Domain.Entities.Catalog, int> repository;

        public CreateCatalogCommandHandler(IMapper mapper,
            IRepositoryAsync<Domain.Entities.Catalog, int> repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<IResult<int>> Handle(CreateCatalogCommand request,
            CancellationToken cancellationToken)
        {
            var catalogDb = await
                repository.GetPagedAsync(1, 1, x => x.Name == request.Name, default);

            if (catalogDb.Any()) {
                return await Result<int>.FailAsync($"El producto {request.Name} " +
                    $"ya se encuentra en la base de datos");
            }
            
            var catalog = mapper.Map<Domain.Entities.Catalog>(request);

            await repository.AddAsync(catalog);

            await repository.SaveChangesAsync();

            return await Result<int>.SuccessAsync(catalog.Id, "");
        }
    }

}
