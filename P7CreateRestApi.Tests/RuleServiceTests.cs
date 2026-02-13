using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Data.Repositories;
using P7CreateRestApi.Entities;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests
{
    public class RuleServiceTests
    {
        private readonly Mock<IRuleRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly RuleService _service;

        public RuleServiceTests()
        {
            _repoMock = new Mock<IRuleRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new RuleService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllRules_ReturnsMappedList()
        {
            // Arrange
            var entities = new List<Rule>
        {
            new Rule { Id = 1 },
            new Rule { Id = 2 }
        };

            var vms = new List<RuleViewModel>
        {
            new RuleViewModel { Id = 1 },
            new RuleViewModel { Id = 2 }
        };

            _repoMock.Setup(r => r.GetAllRules()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<IEnumerable<RuleViewModel>>(entities))
                       .Returns(vms);

            // Act
            var result = await _service.GetAllRules();

            // Assert
            result.Should().BeEquivalentTo(vms);
            _repoMock.Verify(r => r.GetAllRules(), Times.Once);
        }

        [Fact]
        public async Task GetRuleById_ExistingId_ReturnsMappedVm()
        {
            // Arrange
            var entity = new Rule { Id = 1 };
            var vm = new RuleViewModel { Id = 1 };

            _repoMock.Setup(r => r.GetRuleById(1)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<RuleViewModel>(entity)).Returns(vm);

            // Act
            var result = await _service.GetRuleById(1);

            // Assert
            result.Should().Be(vm);
        }

        [Fact]
        public async Task GetRuleById_NotFound_ReturnsNull()
        {
            // Arrange
            _repoMock.Setup(r => r.GetRuleById(1))
                     .ReturnsAsync((Rule?)null);

            // Act
            var result = await _service.GetRuleById(1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task SaveRule_ShouldCallRepoAndReturnVm()
        {
            // Arrange
            var vm = new RuleViewModel { Id = 1 };
            var entity = new Rule { Id = 1 };

            _mapperMock.Setup(m => m.Map<Rule>(vm)).Returns(entity);
            _mapperMock.Setup(m => m.Map<RuleViewModel>(entity)).Returns(vm);

            // Act
            var result = await _service.SaveRule(vm);

            // Assert
            _repoMock.Verify(r => r.SaveRule(entity), Times.Once);
            result.Should().Be(vm);
        }

        [Fact]
        public async Task UpdateRule_ExistingEntity_ReturnsUpdatedVm()
        {
            // Arrange
            var vm = new RuleViewModel { Id = 1 };
            var existing = new Rule { Id = 1 };
            var updatedVm = new RuleViewModel { Id = 1 };

            _repoMock.Setup(r => r.GetRuleById(1)).ReturnsAsync(existing);
            _repoMock.Setup(r => r.Update(existing)).ReturnsAsync(existing);

            _mapperMock.Setup(m => m.Map(vm, existing));
            _mapperMock.Setup(m => m.Map<RuleViewModel>(existing))
                       .Returns(updatedVm);

            // Act
            var result = await _service.UpdateRule(vm);

            // Assert
            _repoMock.Verify(r => r.Update(existing), Times.Once);
            result.Should().Be(updatedVm);
        }

        [Fact]
        public async Task UpdateRule_NotFound_ReturnsNull()
        {
            // Arrange
            var vm = new RuleViewModel { Id = 1 };

            _repoMock.Setup(r => r.GetRuleById(1))
                     .ReturnsAsync((Rule?)null);

            // Act
            var result = await _service.UpdateRule(vm);

            // Assert
            result.Should().BeNull();
            _repoMock.Verify(r => r.Update(It.IsAny<Rule>()), Times.Never);
        }

        [Fact]
        public async Task DeleteRule_ExistingEntity_CallsRepoDelete()
        {
            // Arrange
            var entity = new Rule { Id = 1 };

            _repoMock.Setup(r => r.GetRuleById(1))
                     .ReturnsAsync(entity);

            // Act
            await _service.DeleteRule(1);

            // Assert
            _repoMock.Verify(r => r.Delete(entity), Times.Once);
        }

        [Fact]
        public async Task DeleteRule_NotFound_DoesNothing()
        {
            // Arrange
            _repoMock.Setup(r => r.GetRuleById(1))
                     .ReturnsAsync((Rule?)null);

            // Act
            await _service.DeleteRule(1);

            // Assert
            _repoMock.Verify(r => r.Delete(It.IsAny<Rule>()), Times.Never);
        }
    }
}
