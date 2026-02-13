using AutoMapper;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Data.Repositories;
using P7CreateRestApi.Entities;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests
{
    public class CurvePointServiceTests
    {
        private readonly Mock<ICurvePointRepository> _repoMock;
        private readonly IMapper _mapper;
        private readonly CurvePointService _service;

        public CurvePointServiceTests()
        {
            _repoMock = new Mock<ICurvePointRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CurvePoint, CurvePointViewModel>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            _service = new CurvePointService(_repoMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllCurvePoints_ReturnsMappedList()
        {
            // Arrange
            var entities = new List<CurvePoint>
            {
                new CurvePoint { Id = 1, Term = 1, CurvePointValue = 10 },
                new CurvePoint { Id = 2, Term = 2, CurvePointValue = 20 }
            };
            _repoMock.Setup(r => r.GetAllCurvePoints()).ReturnsAsync(entities);

            // Act
            var result = await _service.GetAllCurvePoints();

            // Assert
            result.Should().HaveCount(2);
            result.First().Id.Should().Be(1);
            _repoMock.Verify(r => r.GetAllCurvePoints(), Times.Once);
        }

        [Fact]
        public async Task GetCurvePointById_ExistingId_ReturnsMappedVm()
        {
            // Arrange
            var entity = new CurvePoint { Id = 1, Term = 5, CurvePointValue = 100 };
            _repoMock.Setup(r => r.GetCurvePointById(1)).ReturnsAsync(entity);

            // Act
            var result = await _service.GetCurvePointById(1);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
            _repoMock.Verify(r => r.GetCurvePointById(1), Times.Once);
        }

        [Fact]
        public async Task GetCurvePointById_NotFound_ReturnsNull()
        {
            // Arrange
            _repoMock.Setup(r => r.GetCurvePointById(1)).ReturnsAsync((CurvePoint?)null);

            // Act
            var result = await _service.GetCurvePointById(1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task SaveCurvePoint_ValidVm_CallsRepoAndReturnsVm()
        {
            // Arrange
            var vm = new CurvePointViewModel { Id = 1, Term = 5, CurvePointValue = 100 };

            // Act
            var result = await _service.SaveCurvePoint(vm);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            _repoMock.Verify(r => r.SaveCurvePoint(It.IsAny<CurvePoint>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCurvePoint_ExistingEntity_ReturnsUpdatedVm()
        {
            // Arrange
            var vm = new CurvePointViewModel { Id = 1, Term = 10, CurvePointValue = 200 };
            var existing = new CurvePoint { Id = 1, Term = 5, CurvePointValue = 100 };

            _repoMock.Setup(r => r.GetCurvePointById(1)).ReturnsAsync(existing);
            _repoMock.Setup(r => r.Update(existing)).ReturnsAsync(existing);

            // Act
            var result = await _service.UpdateCurvePoint(vm);

            // Assert
            result.Should().NotBeNull();
            result!.Term.Should().Be(10);
            _repoMock.Verify(r => r.Update(existing), Times.Once);
        }

        [Fact]
        public async Task UpdateCurvePoint_NotFound_ReturnsNull()
        {
            // Arrange
            var vm = new CurvePointViewModel { Id = 1, Term = 10, CurvePointValue = 200 };
            _repoMock.Setup(r => r.GetCurvePointById(1)).ReturnsAsync((CurvePoint?)null);

            // Act
            var result = await _service.UpdateCurvePoint(vm);

            // Assert
            result.Should().BeNull();
            _repoMock.Verify(r => r.Update(It.IsAny<CurvePoint>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCurvePoint_ExistingEntity_CallsRepoDelete()
        {
            // Arrange
            var existing = new CurvePoint { Id = 1, Term = 5, CurvePointValue = 100 };
            _repoMock.Setup(r => r.GetCurvePointById(1)).ReturnsAsync(existing);

            // Act
            await _service.DeleteCurvePoint(1);

            // Assert
            _repoMock.Verify(r => r.Delete(existing), Times.Once);
        }

        [Fact]
        public async Task DeleteCurvePoint_NotFound_DoesNotCallRepoDelete()
        {
            // Arrange
            _repoMock.Setup(r => r.GetCurvePointById(1)).ReturnsAsync((CurvePoint?)null);

            // Act
            await _service.DeleteCurvePoint(1);

            // Assert
            _repoMock.Verify(r => r.Delete(It.IsAny<CurvePoint>()), Times.Never);
        }
    }
}