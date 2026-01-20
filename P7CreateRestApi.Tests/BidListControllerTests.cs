using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Application.Controllers;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests
{
    public class BidListControllerTests
    {
        // Mock du service pour simuler les interactions sans toucher à la DB
        private readonly Mock<IBidListService> _serviceMock;
        // Instance du controller que l'on va tester
        private readonly BidListController _controller;

        public BidListControllerTests()
        {
            _serviceMock = new Mock<IBidListService>();
            _controller = new BidListController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetBidById_ExistingId_ReturnsOk_WithEntity()
        {
            // Arrange
            var entity = new BidList { Id = 1, Account = "test1", BidQuantity = 1 };  // Création d'une entité BidList
            _serviceMock.Setup(s => s.GetBidId(1)).ReturnsAsync(entity); // Quand le controller appellera GetBidId(1),
                                                                         // le service simulé retournera l'entité créée ci-dessus

            // Act
            var result = await _controller.GetBidById(1);  // Simule un appel HTTP GET /BidList/1

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;  // Vérifie que le résultat est bien un OkObjectResult (HTTP 200)
                                                                              // "Which" permet de récupérer directement l'objet casté

            okResult.Value.Should().Be(entity);  // Vérifie que l'objet retourné dans la réponse
                                                 // est bien l'entité renvoyée par le service mocké

        }
        [Fact]
        public async Task GetBidById_NotFound_ReturnsNotFound()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetBidId(2))// Le service retourne null pour simuler un ID inexistant
            .ReturnsAsync((BidList?)null);

            // Act
            var result = await _controller.GetBidById(2); // Appel du controller avec un ID qui n'existe pas

            // Assert
            result.Should().BeOfType<NotFoundResult>(); // Le controller doit retourner un HTTP 404
        }

        [Fact]
        public async Task UpdateBid_ValidId_ReturnsOk()
        {
            // Arrange
            // ViewModel valide (au minimum pour le test)
            var vm = new BidListViewModel { Id = 1 };

            // Entité existante simulée
            var entity = new BidList { Id = 1 };

            // Le service trouve bien l'entité
            _serviceMock.Setup(s => s.GetBidId(1))
                        .ReturnsAsync(entity);

            // Le service met à jour et retourne l'entité
            _serviceMock.Setup(s => s.UpdateBidList(vm))
                        .ReturnsAsync(entity);

            // Act
            var result = await _controller.UpdateBid(1, vm);

            // Assert
            // Vérifie que la réponse est HTTP 200 OK
            result.Should().BeOfType<OkResult>();

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
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteBid_ExistingId_ReturnsOk()
        {
            // Arrange
            // Entité existante simulée
            var entity = new BidList { Id = 1 };

            // Le service trouve l'entité
            _serviceMock.Setup(s => s.GetBidId(1))
                        .ReturnsAsync(entity);

            // Le delete se passe correctement
            _serviceMock.Setup(s => s.DeleteBidList(1))
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteBid(1);

            // Assert
            // Vérifie que la suppression retourne HTTP 200
            result.Should().BeOfType<OkResult>();

            // Vérifie que la méthode DeleteBidList a bien été appelée une fois
            _serviceMock.Verify(s => s.DeleteBidList(1), Times.Once);
        }

        [Fact]
        public async Task DeleteBid_NotFound_ReturnsNotFound()
        {
            // Arrange
            // Le service ne trouve pas l'entité
            _serviceMock.Setup(s => s.GetBidId(1))
                        .ReturnsAsync((BidList?)null);

            // Act
            var result = await _controller.DeleteBid(1);

            // Assert
            // Le controller doit retourner HTTP 404
            result.Should().BeOfType<NotFoundResult>();
        }


    }
}