using AutoMapper;
using DWShop.Infrastructure.Repositories;
using MediatR;


namespace DWShop.Application.Features.Catalog.Commands.Create
{

    public class CreateCatalogCommandHandler :
        IRequestHandler<CreateCatalogCommand, int>
    {
        private readonly IMapper mapper;
        private readonly IRepositoryAsync<Domain.Entities.Catalog, int> repository;

        public CreateCatalogCommandHandler(IMapper mapper,
            IRepositoryAsync<Domain.Entities.Catalog, int> repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task<int> Handle(CreateCatalogCommand request,
            CancellationToken cancellationToken)
        {
            var catalog = mapper.Map<Domain.Entities.Catalog>(request);

            await repository.AddAsync(catalog);

            await repository.SaveChangesAsync();

            return catalog.Id;
        }
    }
}
