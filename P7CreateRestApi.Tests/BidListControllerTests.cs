using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.Controllers;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests
{
    public class BidListControllerTests
    {
        // Mock du service
        private readonly Mock<IBidListService> _serviceMock;

        // Instance du controller que l'on teste
        private readonly BidListController _controller;

        public BidListControllerTests()
        {
            _serviceMock = new Mock<IBidListService>();
            _controller = new BidListController(_serviceMock.Object);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var vm = new BidListViewModel();
            _controller.ModelState.AddModelError("Account", "Required");

            // Act
            var result = await _controller.Create(vm);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Create_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var entity = new BidList
            {
                Id = 1,
                Account = "ACC1",
                BidQuantity = 10
            };

            var vm = new BidListViewModel
            {
                Id = 1,
                Account = "ACC1",
                BidQuantity = 10
            };

            _serviceMock.Setup(s => s.SaveBidList(vm))
                        .ReturnsAsync(entity);

            // Act
            var result = await _controller.Create(vm);

            // Assert
            // createdResult contient donc la réponse HTTP 201 retournée par le controller.
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Which;

            createdResult.ActionName.Should().Be(nameof(_controller.GetBidById));
            createdResult.RouteValues!["id"].Should().Be(entity.Id);
            createdResult.Value.Should().Be(entity);
        }

        [Fact]
        public async Task GetBidById_ExistingId_ReturnsOk_WithEntity()
        {
            // Arrange
            // Création entité BidList
            var entity = new BidList { Id = 1, Account = "test1", BidQuantity = 1 };
            // Setup du comportement attendu
            _serviceMock.Setup(s => s.GetBidId(1)).ReturnsAsync(entity);

            // Act
            // Simule un appel HTTP GET /BidList/1
            var result = await _controller.GetBidById(1);

            // Assert
            // Vérifie que le résultat est bien un OkObjectResult (HTTP 200)
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            // Vérifie que l'objet retourné est bien l'entité                                                               
            okResult.Value.Should().Be(entity);
        }

        [Fact]
        public async Task GetBidById_NotFound_ReturnsNotFound()
        {
            // Arrange
            // Simule un ID inexistant, retourne null
            _serviceMock.Setup(s => s.GetBidId(2))
            .ReturnsAsync((BidList?)null);

            // Act
            // Appel du controller avec un ID qui n'existe pas
            var result = await _controller.GetBidById(2);

            // Assert
            // Le controller doit retourner un reponse HTTP 404
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateBid_ValidId_ReturnsOk()
        {
            // Arrange
            // ViewModel valide
            var vm = new BidListViewModel { Id = 1, Account = "XXXYYY", BidQuantity = 5 };

            // Entité existante simulée
            var entity = new BidList { Id = 1, Account = "XXXYYY", BidQuantity = 5 };

            // Simule que l'entité est bien retournée
            _serviceMock.Setup(s => s.GetBidId(1))
                        .ReturnsAsync(entity);

            // Simule la mise à jour et retourne l'entité
            _serviceMock.Setup(s => s.UpdateBidList(vm))
                        .ReturnsAsync(entity);

            // Act
            var result = await _controller.UpdateBid(1, vm);

            // Assert
            // Vérifie que la réponse est HTTP 200 OK
            result.Should().BeOfType<OkObjectResult>();

            // Vérifie que la méthode UpdateBidList a bien été appelée une seule fois
            _serviceMock.Verify(s => s.UpdateBidList(vm), Times.Once);
        }

        [Fact]
        public async Task UpdateBid_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            // L'ID du ViewModel ne correspond pas à l'ID de l'URL
            var vm = new BidListViewModel { Id = 2 };

            // Act
            var result = await _controller.UpdateBid(1, vm);

            // Assert
            // Le controller doit bloquer la requête
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateBid_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var vm = new BidListViewModel { Id = 1, Account = "TEST", BidQuantity = 5 };
            _controller.ModelState.AddModelError("Account", "Required");

            // Act
            var result = await _controller.UpdateBid(1, vm);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateBid_IdZero_ReturnsBadRequest()
        {
            // Arrange
            var vm = new BidListViewModel { Id = 0, Account = "TEST", BidQuantity = 5 };

            // Act
            var result = await _controller.UpdateBid(1, vm);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateBid_ServiceReturnsNull_ReturnsNotFound()
        {
            // Arrange
            var vm = new BidListViewModel { Id = 1, Account = "TEST", BidQuantity = 5 };

            // Simule que la mise à jour retourne null
            _serviceMock.Setup(s => s.UpdateBidList(vm))
                        .ReturnsAsync((BidList?)null);

            // Act
            var result = await _controller.UpdateBid(1, vm);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteBid_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var entity = new BidList { Id = 1 };

            // Simule que l'on trouve l'entité
            _serviceMock.Setup(s => s.GetBidId(1))
                        .ReturnsAsync(entity);
            // Simule que la suppression est complétée
            _serviceMock.Setup(s => s.DeleteBidList(1))
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteBid(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _serviceMock.Verify(s => s.DeleteBidList(1), Times.Once);
        }

        [Fact]
        public async Task DeleteBid_NotFound_ReturnsNotFound()
        {
            // Arrange
            // Simule que l'on ne trouve pas l'entité
            _serviceMock.Setup(s => s.GetBidId(1))
                        .ReturnsAsync((BidList?)null);

            // Act
            var result = await _controller.DeleteBid(1);

            // Assert
            // Le controller doit retourner une reponse HTTP 404
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}