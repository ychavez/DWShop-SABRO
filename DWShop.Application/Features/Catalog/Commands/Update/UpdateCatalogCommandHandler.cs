
using AutoMapper;
using DWShop.Infrastructure.Repositories;
using DWShop.Shared.Wrapper;
using MediatR;

namespace DWShop.Application.Features.Catalog.Commands.Update
{
    public class UpdateCatalogCommandHandler : IRequestHandler<UpdateCatalogCommand, IResult>
    {
        private readonly IRepositoryAsync<Domain.Entities.Catalog, int> repositoryAsync;
       

        public UpdateCatalogCommandHandler(
            IRepositoryAsync<Domain.Entities.Catalog,int> repositoryAsync
            )
        {
            this.repositoryAsync = repositoryAsync;
        }
        public async Task<IResult> Handle(UpdateCatalogCommand request, 
            CancellationToken cancellationToken)
        {
            var entity = await repositoryAsync.GetByIdAsync(request.Id);

            if (entity is null)
                return await Result.FailAsync("Producto no encontrado");

            entity.PhotoURL = request.PhotoURL;
            entity.Summary = request.Summary;
            entity.Category = request.Category;
            entity.Description = request.Description;
            entity.Name = request.Name;

            await repositoryAsync.UpdateAsync(entity);
            await repositoryAsync.SaveChangesAsync();

            return await Result.SuccessAsync();           
        }
    }
}
