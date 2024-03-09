using AutoMapper;
using DWShop.Application.Features.Catalog.Queries;
using DWShop.Application.Responses.Catalog;
using DWShop.Infrastructure.Repositories;
using Moq;

namespace DWShop.Test.Application.Features.Catalog
{
    public class GetCatalogQueryTest
    {

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepositoryAsync<Domain.Entities.Catalog,int>> _repositoryAsync;
        public GetCatalogQueryTest()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryAsync = new Mock<IRepositoryAsync<Domain.Entities.Catalog, int>>();
        }


        [Fact]
        public async Task Handle_Should_Return_Result()
        {
            //Arrange
            var query = new GetCatalogQuery();
            var catalogs = new List<Domain.Entities.Catalog>()
            {
            new ()
                {
                    Name = "Teclado",
                    Category = "Computacion",
                    Id = 1,
                    PhotoURL = "photo",
                    Price = 12.5M,
                    Summary = "Bonito teclado mecanico iluminado",
                    Description = "Teclado marca corsair mecanico teclas azules"
                },
                new ()
                {
                    Name = "Mouse",
                    Category = "Computacion",
                    Id = 2,
                    PhotoURL = "photo",
                    Price = 120.5M,
                    Summary = "Bonito mouse iluminado",
                    Description = "Mouse"
                }
            };

            var response = new List<CatalogResponse>
            {
                new() {
                    Name = "Teclado",
                    Category = "Computacion",
                    Id = 1,
                    PhotoURL = "photo",
                    Price = 12.5M,
                    Summary = "Bonito teclado mecanico iluminado",
                    Description = "Teclado marca corsair mecanico teclas azules"
                },
                new ()
                {
                    Name = "Mouse",
                    Category = "Computacion",
                    Id = 2,
                    PhotoURL = "photo",
                    Price = 120.5M,
                    Summary = "Bonito mouse iluminado",
                    Description = "Mouse"
                }

            };

            _mapperMock.Setup(x=> x.Map<List<CatalogResponse>>(catalogs)).Returns(response);

            _repositoryAsync.Setup(x => x.GetAllAsync()).ReturnsAsync(catalogs);

            var handler = new GetCatalogQueryHandler(_repositoryAsync.Object, _mapperMock.Object);

            //Act
            var result = await handler.Handle(query, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(response,result.Data);
            Assert.True(result.Succeded);
            _mapperMock.Verify(x =>
            x.Map<List<CatalogResponse>>(It.IsAny<List<Domain.Entities.Catalog>>()), Times.Once);
            
            _repositoryAsync.Verify(x=> x.GetAllAsync(), Times.Once);


        }
    }
}
