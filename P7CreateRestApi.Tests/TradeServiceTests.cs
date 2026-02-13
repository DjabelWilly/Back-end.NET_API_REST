using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Data.Repositories;
using P7CreateRestApi.Entities;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests
{
    public class TradeServiceTests
    {
        private readonly Mock<ITradeRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TradeService _service;

        public TradeServiceTests()
        {
            _repoMock = new Mock<ITradeRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new TradeService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllTrades_ReturnsMappedList()
        {
            // Arrange
            var entities = new List<Trade>
        {
            new Trade { Id = 1 },
            new Trade { Id = 2 }
        };

            var vms = new List<TradeViewModel>
        {
            new TradeViewModel { Id = 1 },
            new TradeViewModel { Id = 2 }
        };

            _repoMock.Setup(r => r.GetAllTrades()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<IEnumerable<TradeViewModel>>(entities))
                       .Returns(vms);

            // Act
            var result = await _service.GetAllTrades();

            // Assert
            result.Should().BeEquivalentTo(vms);
            _repoMock.Verify(r => r.GetAllTrades(), Times.Once);
        }

        [Fact]
        public async Task GetTradeById_ExistingId_ReturnsMappedVm()
        {
            // Arrange
            var entity = new Trade { Id = 1 };
            var vm = new TradeViewModel { Id = 1 };

            _repoMock.Setup(r => r.GetTradeById(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<TradeViewModel>(entity)).Returns(vm);

            // Act
            var result = await _service.GetTradeById(1);

            // Assert
            result.Should().Be(vm);
        }

        [Fact]
        public async Task GetTradeById_NotFound_ReturnsNull()
        {
            // Arrange
            _repoMock.Setup(r => r.GetTradeById(1))
                     .ReturnsAsync((Trade?)null);

            // Act
            var result = await _service.GetTradeById(1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task SaveTrade_ShouldCallRepoAndReturnVm()
        {
            // Arrange
            var vm = new TradeViewModel { Id = 1 };
            var entity = new Trade { Id = 1 };

            _mapperMock.Setup(m => m.Map<Trade>(vm)).Returns(entity);
            _mapperMock.Setup(m => m.Map<TradeViewModel>(entity)).Returns(vm);

            // Act
            var result = await _service.SaveTrade(vm);

            // Assert
            _repoMock.Verify(r => r.SaveTrade(entity), Times.Once);
            result.Should().Be(vm);
        }

        [Fact]
        public async Task UpdateTrade_ExistingEntity_ReturnsUpdatedVm()
        {
            // Arrange
            var vm = new TradeViewModel { Id = 1 };
            var existing = new Trade { Id = 1 };
            var updatedVm = new TradeViewModel { Id = 1 };

            _repoMock.Setup(r => r.GetTradeById(1)).ReturnsAsync(existing);
            _repoMock.Setup(r => r.UpdateTrade(existing)).ReturnsAsync(existing);

            _mapperMock.Setup(m => m.Map(vm, existing));
            _mapperMock.Setup(m => m.Map<TradeViewModel>(existing))
                       .Returns(updatedVm);

            // Act
            var result = await _service.UpdateTrade(vm);

            // Assert
            _repoMock.Verify(r => r.UpdateTrade(existing), Times.Once);
            result.Should().Be(updatedVm);
        }

        [Fact]
        public async Task UpdateTrade_NotFound_ReturnsNull()
        {
            // Arrange
            var vm = new TradeViewModel { Id = 1 };

            _repoMock.Setup(r => r.GetTradeById(1))
                     .ReturnsAsync((Trade?)null);

            // Act
            var result = await _service.UpdateTrade(vm);

            // Assert
            result.Should().BeNull();
            _repoMock.Verify(r => r.UpdateTrade(It.IsAny<Trade>()), Times.Never);
        }

        [Fact]
        public async Task DeleteTrade_ExistingEntity_CallsRepoDelete()
        {
            // Arrange
            var entity = new Trade { Id = 1 };

            _repoMock.Setup(r => r.GetTradeById(1))
                     .ReturnsAsync(entity);

            // Act
            await _service.DeleteTrade(1);

            // Assert
            _repoMock.Verify(r => r.DeleteTrade(entity), Times.Once);
        }

        [Fact]
        public async Task DeleteTrade_NotFound_DoesNothing()
        {
            // Arrange
            _repoMock.Setup(r => r.GetTradeById(1))
                     .ReturnsAsync((Trade?)null);

            // Act
            await _service.DeleteTrade(1);

            // Assert
            _repoMock.Verify(r => r.DeleteTrade(It.IsAny<Trade>()), Times.Never);
        }
    }
}