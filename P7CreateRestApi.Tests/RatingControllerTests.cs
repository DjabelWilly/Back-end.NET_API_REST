using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.Controllers;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests
{
    public class RatingControllerTests
    {
        private readonly Mock<IRatingService> _serviceMock;
        private readonly RatingController _controller;

        public RatingControllerTests()
        {
            _serviceMock = new Mock<IRatingService>();
            _controller = new RatingController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithList()
        {
            // Arrange
            var list = new List<RatingViewModel>
            {
                new RatingViewModel { Id = 1, MoodysRating = "AA+", SandPRating = "AAA", FitchRating = "AAA", OrderNumber = 1 },
                new RatingViewModel { Id = 2, MoodysRating = "AA-", SandPRating = "AA", FitchRating = "AA", OrderNumber = 2 }
            };
            _serviceMock.Setup(s => s.GetAllRatings()).ReturnsAsync(list);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().Be(list);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsOk()
        {
            // Arrange
            var rating = new RatingViewModel { Id = 1, MoodysRating = "AA+", SandPRating = "AAA", FitchRating = "AAA", OrderNumber = 1 };
            _serviceMock.Setup(s => s.GetRatingById(1)).ReturnsAsync(rating);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().Be(rating);
        }

        [Fact]
        public async Task GetById_NotFound_ReturnsNotFound()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetRatingById(1)).ReturnsAsync((RatingViewModel?)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Create_ValidRating_ReturnsCreated()
        {
            // Arrange
            var vm = new RatingViewModel
            {
                MoodysRating = "AA+",
                SandPRating = "AAA",
                FitchRating = "AAA",
                OrderNumber = 1
            };
           
            _serviceMock.Setup(s => s.SaveRating(vm)).ReturnsAsync(vm);

            // Act
            var result = await _controller.Create(vm);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Which;
            createdResult.Value.Should().Be(vm);
            createdResult.ActionName.Should().Be(nameof(_controller.GetById));
            createdResult.RouteValues!["id"].Should().Be(vm.Id);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var vm = new RatingViewModel();
            _controller.ModelState.AddModelError("MoodysRating", "Required");

            // Act
            var result = await _controller.Create(vm);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_ValidId_ReturnsOk()
        {
            // Arrange
            var vm = new RatingViewModel
            {
                Id = 1,
                MoodysRating = "AA+",
                SandPRating = "AAA",
                FitchRating = "AAA",
                OrderNumber = 1
            };
            var existing = new RatingViewModel { Id = 1, MoodysRating = "Old", SandPRating = "Old", FitchRating = "Old", OrderNumber = 1 };

            _serviceMock.Setup(s => s.GetRatingById(1)).ReturnsAsync(existing);
            _serviceMock.Setup(s => s.UpdateRating(vm)).ReturnsAsync(vm);

            // Act
            var result = await _controller.Update(1, vm);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().Be(vm);
        }

        [Fact]
        public async Task Update_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var vm = new RatingViewModel { Id = 1 };
            _controller.ModelState.AddModelError("MoodysRating", "Required");

            // Act
            var result = await _controller.Update(1, vm);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_IdZero_ReturnsBadRequest()
        {
            // Arrange
            var vm = new RatingViewModel { Id = 0 };

            // Act
            var result = await _controller.Update(1, vm);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_IdMismatch_ReturnsBadRequest()
        {
            // Arrange
            var vm = new RatingViewModel { Id = 2 };

            // Act
            var result = await _controller.Update(1, vm);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_NotFound_ReturnsNotFound()
        {
            // Arrange
            var vm = new RatingViewModel { Id = 1 };
            _serviceMock.Setup(s => s.GetRatingById(1)).ReturnsAsync((RatingViewModel?)null);

            // Act
            var result = await _controller.Update(1, vm);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var existing = new RatingViewModel { Id = 1 };
            _serviceMock.Setup(s => s.GetRatingById(1)).ReturnsAsync(existing);
            _serviceMock.Setup(s => s.DeleteRating(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _serviceMock.Verify(s => s.DeleteRating(1), Times.Once);
        }

        [Fact]
        public async Task Delete_NotFound_ReturnsNotFound()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetRatingById(1)).ReturnsAsync((RatingViewModel?)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
