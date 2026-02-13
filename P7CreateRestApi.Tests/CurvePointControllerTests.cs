using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.Controllers;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests
{
    public class CurvePointControllerTests
    {
        private readonly Mock<ICurvePointService> _serviceMock;
        private readonly CurvePointController _controller;

        public CurvePointControllerTests()
        {
            _serviceMock = new Mock<ICurvePointService>();
            _controller = new CurvePointController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetCurvePointById_ExistingId_ReturnsOk()
        {
            // Arrange
            var vm = new CurvePointViewModel
            {
                Id = 1,
                CurveId = 10,
                Term = 5,
                CurvePointValue = 100
            };

            _serviceMock.Setup(s => s.GetCurvePointById(1))
                        .ReturnsAsync(vm);

            // Act
            var result = await _controller.GetCurvePointById(1);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().Be(vm);
        }

        [Fact]
        public async Task GetCurvePointById_NotFound_ReturnsNotFound()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetCurvePointById(1))
                        .ReturnsAsync((CurvePointViewModel?)null);

            // Act
            var result = await _controller.GetCurvePointById(1);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetAllCurvePoints_ReturnsOk_WithList()
        {
            // Arrange
            var list = new List<CurvePointViewModel>
            {
                new CurvePointViewModel { Id = 1, CurveId = 10, Term = 1 },
                new CurvePointViewModel { Id = 2, CurveId = 20, Term = 2 }
            };

            _serviceMock.Setup(s => s.GetAllCurvePoints())
                        .ReturnsAsync(list);

            // Act
            var result = await _controller.GetAllCurvePoints();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().BeEquivalentTo(list);
        }

        [Fact]
        public async Task Create_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var vm = new CurvePointViewModel
            {
                CurveId = 10,
                Term = 5,
                CurvePointValue = 100
            };

            _serviceMock.Setup(s => s.SaveCurvePoint(vm))
                .ReturnsAsync(vm);

            // Act
            var result = await _controller.Create(vm);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Which;
            createdResult.ActionName.Should().Be(nameof(_controller.GetCurvePointById));
            createdResult.RouteValues!["id"].Should().Be(vm.Id);
            createdResult.Value.Should().Be(vm);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var vm = new CurvePointViewModel();
            _controller.ModelState.AddModelError("CurveId", "Required");

            // Act
            var result = await _controller.Create(vm);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_ValidId_ReturnsOk()
        {
            // Arrange
            var vm = new CurvePointViewModel
            {
                Id = 1,
                CurveId = 10,
                Term = 5,
                CurvePointValue = 200
            };

            _serviceMock.Setup(s => s.GetCurvePointById(1))
                 .ReturnsAsync(vm);

            _serviceMock.Setup(s => s.UpdateCurvePoint(vm))
                        .ReturnsAsync(vm);

            // Act
            var result = await _controller.Update(1, vm);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().Be(vm);

            _serviceMock.Verify(s => s.UpdateCurvePoint(vm), Times.Once);
        }

        [Fact]
        public async Task Update_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var vm = new CurvePointViewModel { Id = 2 };

            // Act
            var result = await _controller.Update(1, vm);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_NotFound_ReturnsNotFound()
        {
            // Arrange
            var vm = new CurvePointViewModel { Id = 1 };

            _serviceMock.Setup(s => s.GetCurvePointById(1))
                        .ReturnsAsync((CurvePointViewModel?)null);

            // Act
            var result = await _controller.Update(1, vm);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Update_InvalidModel_ReturnsBadRequest()
        {
            var vm = new CurvePointViewModel { Id = 1 };
            _controller.ModelState.AddModelError("CurveId", "Required");

            var result = await _controller.Update(1, vm);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var vm = new CurvePointViewModel { Id = 1 };

            _serviceMock.Setup(s => s.GetCurvePointById(1))
                        .ReturnsAsync(vm);

            _serviceMock.Setup(s => s.DeleteCurvePoint(1))
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _serviceMock.Verify(s => s.DeleteCurvePoint(1), Times.Once);
        }

        [Fact]
        public async Task Delete_NotFound_ReturnsNotFound()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetCurvePointById(1))
                        .ReturnsAsync((CurvePointViewModel?)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
