using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Data.Repositories;
using P7CreateRestApi.Entities;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests
{

    public class RatingServiceTests
    {
        private readonly Mock<IRatingRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly RatingService _service;

        public RatingServiceTests()
        {
            _repoMock = new Mock<IRatingRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new RatingService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllRatings_ReturnsMappedList()
        {
            // Arrange
            var entities = new List<Rating>
        {
            new Rating { Id = 1 },
            new Rating { Id = 2 }
        };

            var vms = new List<RatingViewModel>
        {
            new RatingViewModel { Id = 1 },
            new RatingViewModel { Id = 2 }
        };

            _repoMock.Setup(r => r.GetAllRatings()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<IEnumerable<RatingViewModel>>(entities))
                       .Returns(vms);

            // Act
            var result = await _service.GetAllRatings();

            // Assert
            // eEquivalentTo() pour comparer des listes
            result.Should().BeEquivalentTo(vms);
            _repoMock.Verify(r => r.GetAllRatings(), Times.Once);
        }

        [Fact]
        public async Task GetRatingById_ExistingId_ReturnsMappedVm()
        {
            // Arrange
            var entity = new Rating { Id = 1 };
            var vm = new RatingViewModel { Id = 1 };

            _repoMock.Setup(r => r.GetRatingById(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<RatingViewModel>(entity)).Returns(vm);

            // Act
            var result = await _service.GetRatingById(1);

            // Assert
            result.Should().Be(vm);
        }

        [Fact]
        public async Task GetRatingById_NotFound_ReturnsNull()
        {
            // Arrange
            _repoMock.Setup(r => r.GetRatingById(1))
                     .ReturnsAsync((Rating?)null);

            // Act
            var result = await _service.GetRatingById(1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task SaveRating_ShouldCallRepoAndReturnVm()
        {
            // Arrange
            var vm = new RatingViewModel { Id = 1 };
            var entity = new Rating { Id = 1 };

            _mapperMock.Setup(m => m.Map<Rating>(vm)).Returns(entity);
            _mapperMock.Setup(m => m.Map<RatingViewModel>(entity)).Returns(vm);

            // Act
            var result = await _service.SaveRating(vm);

            // Assert
            _repoMock.Verify(r => r.SaveRating(entity), Times.Once);
            result.Should().Be(vm);
        }

        [Fact]
        public async Task UpdateRating_ExistingEntity_ReturnsUpdatedVm()
        {
            // Arrange
            var vm = new RatingViewModel { Id = 1 };
            var existing = new Rating { Id = 1 };
            var updatedVm = new RatingViewModel { Id = 1 };

            _repoMock.Setup(r => r.GetRatingById(1)).ReturnsAsync(existing);
            _repoMock.Setup(r => r.Update(existing)).ReturnsAsync(existing);

            _mapperMock.Setup(m => m.Map(vm, existing));
            _mapperMock.Setup(m => m.Map<RatingViewModel>(existing))
                       .Returns(updatedVm);

            // Act
            var result = await _service.UpdateRating(vm);

            // Assert
            _repoMock.Verify(r => r.Update(existing), Times.Once);
            result.Should().Be(updatedVm);
        }

        [Fact]
        public async Task UpdateRating_NotFound_ReturnsNull()
        {
            // Arrange
            var vm = new RatingViewModel { Id = 1 };

            _repoMock.Setup(r => r.GetRatingById(1))
                     .ReturnsAsync((Rating?)null);

            // Act
            var result = await _service.UpdateRating(vm);

            // Assert
            result.Should().BeNull();
            _repoMock.Verify(r => r.Update(It.IsAny<Rating>()), Times.Never);
        }

        [Fact]
        public async Task DeleteRating_ExistingEntity_CallsRepoDelete()
        {
            // Arrange
            var entity = new Rating { Id = 1 };

            _repoMock.Setup(r => r.GetRatingById(1))
                     .ReturnsAsync(entity);

            // Act
            await _service.DeleteRating(1);

            // Assert
            _repoMock.Verify(r => r.Delete(entity), Times.Once);
        }

        [Fact]
        public async Task DeleteRating_NotFound_DoesNothing()
        {
            // Arrange
            _repoMock.Setup(r => r.GetRatingById(1))
                     .ReturnsAsync((Rating?)null);

            // Act
            await _service.DeleteRating(1);

            // Assert
            _repoMock.Verify(r => r.Delete(It.IsAny<Rating>()), Times.Never);
        }
    }
}