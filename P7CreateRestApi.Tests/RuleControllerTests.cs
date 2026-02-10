using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.Controllers;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests
{
    public class RuleControllerTests
    {
        private readonly Mock<IRuleService> _serviceMock;
        private readonly RuleController _controller;

        public RuleControllerTests()
        {
            _serviceMock = new Mock<IRuleService>();
            _controller = new RuleController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithList()
        {
            var list = new List<Rule>
            {
                new Rule { Id = 1, Name = "Rule1" },
                new Rule { Id = 2, Name = "Rule2" }
            };

            _serviceMock.Setup(s => s.GetAllRules()).ReturnsAsync(list);

            var result = await _controller.GetAll();

            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().BeEquivalentTo(list);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsOk_WithEntity()
        {
            var rule = new Rule { Id = 1, Name = "Rule1" };
            _serviceMock.Setup(s => s.GetRuleById(1)).ReturnsAsync(rule);

            var result = await _controller.GetById(1);

            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().Be(rule);
        }

        [Fact]
        public async Task GetById_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetRuleById(1)).ReturnsAsync((Rule?)null);

            var result = await _controller.GetById(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Create_ValidModel_ReturnsCreatedAtAction()
        {
            var vm = new RuleViewModel
            {
                Name = "Rule1",
                Description = "Desc1",
                Json = "{}",
                Template = "Template1",
                SqlStr = "SELECT 1",
                SqlPart = "WHERE 1=1"
            };
            var created = new Rule { Id = 1, Name = vm.Name };

            _serviceMock.Setup(s => s.SaveRule(vm)).ReturnsAsync(created);

            var result = await _controller.Create(vm);

            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Which;
            createdResult.ActionName.Should().Be(nameof(_controller.GetById));
            createdResult.RouteValues!["id"].Should().Be(created.Id);
            createdResult.Value.Should().Be(created);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsBadRequest()
        {
            var vm = new RuleViewModel(); // vide => ModelState invalide
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Create(vm);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_ValidId_ReturnsOk_WithUpdatedEntity()
        {
            var vm = new RuleViewModel
            {
                Id = 1,
                Name = "Updated",
                Description = "Desc",
                Json = "{}",
                Template = "Template",
                SqlStr = "SELECT 1",
                SqlPart = "WHERE 1=1"
            };
            var existing = new Rule { Id = 1, Name = "Old" };
            var updated = new Rule { Id = 1, Name = "Updated" };

            _serviceMock.Setup(s => s.GetRuleById(1)).ReturnsAsync(existing);
            _serviceMock.Setup(s => s.UpdateRule(vm)).ReturnsAsync(updated);

            var result = await _controller.Update(1, vm);

            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().Be(updated);
            _serviceMock.Verify(s => s.UpdateRule(vm), Times.Once);
        }

        [Fact]
        public async Task Update_IdMismatch_ReturnsBadRequest()
        {
            var vm = new RuleViewModel { Id = 2 };

            var result = await _controller.Update(1, vm);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_NotFound_ReturnsNotFound()
        {
            var vm = new RuleViewModel { Id = 1 };
            _serviceMock.Setup(s => s.GetRuleById(1)).ReturnsAsync((Rule?)null);

            var result = await _controller.Update(1, vm);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Update_InvalidModel_ReturnsBadRequest()
        {
            var vm = new RuleViewModel { Id = 1 };
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.Update(1, vm);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnsNoContent()
        {
            var existing = new Rule { Id = 1 };
            _serviceMock.Setup(s => s.GetRuleById(1)).ReturnsAsync(existing);
            _serviceMock.Setup(s => s.DeleteRule(1)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(1);

            result.Should().BeOfType<NoContentResult>();
            _serviceMock.Verify(s => s.DeleteRule(1), Times.Once);
        }

        [Fact]
        public async Task Delete_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetRuleById(1)).ReturnsAsync((Rule?)null);

            var result = await _controller.Delete(1);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
