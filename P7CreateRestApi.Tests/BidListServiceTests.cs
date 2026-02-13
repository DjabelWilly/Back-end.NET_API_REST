using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Data.Repositories;
using P7CreateRestApi.Entities;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests
{
    public class BidListServiceTests
    {
        private readonly Mock<IBidListRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly BidListService _service;

        public BidListServiceTests()
        {
            _repoMock = new Mock<IBidListRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new BidListService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task SaveBidList_ShouldCallRepoAndReturnVM()
        {
            // Arrange
            var vm = new BidListViewModel { Id = 1, Account = "ACC1" };
            var entity = new BidList { Id = 1, Account = "ACC1" };
            // Set le mock AutoMapper pour retourner l'entity à partir du VM et inversement 
            _mapperMock.Setup(m => m.Map<BidList>(vm)).Returns(entity);
            _mapperMock.Setup(m => m.Map<BidListViewModel>(entity)).Returns(vm);

            // Act
            var result = await _service.SaveBidList(vm);

            // Assert
            //Vérifie que la méthode SaveBidList du repository a été appelée exactement une fois avec l'entity.
            _repoMock.Verify(r => r.SaveBidList(entity), Times.Once);
            // Doit retourner le vm
            result.Should().Be(vm);
        }

        [Fact]
        public async Task GetBidId_ExistingId_ReturnsVM()
        {
            // Arrange
            var entity = new BidList { Id = 1, Account = "ACC1" };
            var vm = new BidListViewModel { Id = 1, Account = "ACC1" };
            _repoMock.Setup(r => r.GetBidListById(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<BidListViewModel>(entity)).Returns(vm);

            // Act
            var result = await _service.GetBidId(1);

            // Assert
            result.Should().Be(vm);
        }

        [Fact]
        public async Task GetBidId_NotFound_ReturnsNull()
        {
            // Arrange
            // set le repository pour qu'il retourne null à l'id 1
            _repoMock.Setup(r => r.GetBidListById(1)).ReturnsAsync((BidList?)null);

            // Act
            // Appelle le service avec id = 1
            var result = await _service.GetBidId(1);

            // Assert
            // Doit retourner null
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateBidList_ExistingEntity_ReturnsUpdatedVM()
        {
            // Arrange
            // Prépare le ViewModel à mettre à jour, l'entity existante et le VM mis à jour attendu.
            var vm = new BidListViewModel { Id = 1, Account = "New" };
            var entity = new BidList { Id = 1, Account = "Old" };
            var updatedVM = new BidListViewModel { Id = 1, Account = "New" };

            _repoMock.Setup(r => r.GetBidListById(1)).ReturnsAsync(entity);
            _repoMock.Setup(r => r.Update(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map(vm, entity)); // applique les modifications du VM sur l'entity existante
            _mapperMock.Setup(m => m.Map<BidListViewModel>(entity)).Returns(updatedVM);

            // Act
            var result = await _service.UpdateBidList(vm);

            // Assert
            // Vérifie que la méthode Update du repo a été appelée
            _repoMock.Verify(r => r.Update(entity), Times.Once);

            // Vérifie que le résultat est le ViewModel mis à jour.
            result.Should().Be(updatedVM);
        }

        [Fact]
        public async Task UpdateBidList_NotFound_ReturnsNull()
        {
            // Arrange
            var vm = new BidListViewModel { Id = 1 };
            // Set le repo pour retourner null à id = 1
            _repoMock.Setup(r => r.GetBidListById(1)).ReturnsAsync((BidList?)null);

            // Act
            var result = await _service.UpdateBidList(vm);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteBidList_ExistingEntity_CallsRepo()
        {
            // Arrange
            var entity = new BidList { Id = 1 };
            // Set le repo pour retourner l'entity à l'id 1
            _repoMock.Setup(r => r.GetBidListById(1)).ReturnsAsync(entity);

            // Act
            await _service.DeleteBidList(1);

            // Assert
            // Vérifie que la méthode Delete du repo a été appelée une fois sur l'entity existante.
            _repoMock.Verify(r => r.Delete(entity), Times.Once);
        }

        [Fact]
        public async Task DeleteBidList_NotFound_DoesNothing()
        {
            // Arrange
            // Configure le repo pour qu'il retourne null pour l'Id 1 inexistant.
            _repoMock.Setup(r => r.GetBidListById(1)).ReturnsAsync((BidList?)null);

            // Act
            await _service.DeleteBidList(1);

            // Assert
            // Vérifie que Delete n'a jamais été appelé puisque l'entity n'existe pas.
            _repoMock.Verify(r => r.Delete(It.IsAny<BidList>()), Times.Never);
        }
    }
}