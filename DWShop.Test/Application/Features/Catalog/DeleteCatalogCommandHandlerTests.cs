using DWShop.Application.Features.Catalog.Commands.Delete;
using DWShop.Infrastructure.Repositories;
using Moq;

namespace DWShop.Test.Application.Features.Catalog
{
    public class DeleteCatalogCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidId_ReturnsSuccessResult()
        {
            // Arrange
            int validCatalogId = 1;
            var mockRepository = new Mock<IRepositoryAsync<Domain.Entities.Catalog, int>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(validCatalogId))
                          .ReturnsAsync(new Domain.Entities.Catalog { Id = validCatalogId });
            var handler = new DelteCatalogCommandHandler(mockRepository.Object);

            // Act
            var result = await handler.Handle(new DeleteCatalogCommand { Id = validCatalogId }, CancellationToken.None);

            // Assert
            Assert.True(result.Succeded);
            mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Domain.Entities.Catalog>()), Times.Once);
            mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsFailureResult()
        {
            // Arrange
            int invalidCatalogId = -1; // Assuming -1 is an invalid Id
            var mockRepository = new Mock<IRepositoryAsync<Domain.Entities.Catalog, int>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(invalidCatalogId))
                          .ReturnsAsync((Domain.Entities.Catalog)null); // Simulate not found
            var handler = new DelteCatalogCommandHandler(mockRepository.Object);

            // Act
            var result = await handler.Handle(new DeleteCatalogCommand { Id = invalidCatalogId }, CancellationToken.None);

            // Assert
            Assert.False(result.Succeded);
            Assert.Equal("El Producto no existe", result.Messages[0]);
            mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Domain.Entities.Catalog>()), Times.Never);
            mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never);
        }
    }
}
