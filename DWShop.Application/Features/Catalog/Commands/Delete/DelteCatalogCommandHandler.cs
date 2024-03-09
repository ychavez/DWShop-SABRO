using DWShop.Infrastructure.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Catalog.Commands.Delete
{
    public class DelteCatalogCommandHandler :
        IRequestHandler<DeleteCatalogCommand, IResult>
    {
        private readonly IRepositoryAsync<Domain.Entities.Catalog, int> repository;

        public DelteCatalogCommandHandler(IRepositoryAsync<Domain.Entities.Catalog,int>
            repository)
        {
            this.repository = repository;
        }
        public async Task<IResult> Handle(DeleteCatalogCommand request,
            CancellationToken cancellationToken)
        {
           var catalog = await repository.GetByIdAsync(request.Id);

            if (catalog is null)
                return await Result.FailAsync("El Producto no existe");

            await repository.DeleteAsync(catalog);

            await repository.SaveChangesAsync();

            return await Result.SuccessAsync();
        }
    }
}
