using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.Controllers;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Tests
{
    public class TradeControllerTests
    {
        private readonly Mock<ITradeService> _serviceMock;
        private readonly TradeController _controller;

        public TradeControllerTests()
        {
            _serviceMock = new Mock<ITradeService>();
            _controller = new TradeController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllTrades_ReturnsOk_WithList()
        {
            var list = new List<Trade>
            {
                new Trade { Id = 1, Account = "ACC1" },
                new Trade { Id = 2, Account = "ACC2" }
            };

            _serviceMock.Setup(s => s.GetAllTrades()).ReturnsAsync(list);

            var result = await _controller.GetAllTrades();

            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().BeEquivalentTo(list);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsOk_WithEntity()
        {
            var trade = new Trade { Id = 1, Account = "ACC1" };
            _serviceMock.Setup(s => s.GetTradeById(1)).ReturnsAsync(trade);

            var result = await _controller.GetById(1);

            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().Be(trade);
        }

        [Fact]
        public async Task GetById_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetTradeById(1)).ReturnsAsync((Trade?)null);

            var result = await _controller.GetById(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Create_ValidModel_ReturnsCreatedAtAction()
        {
            var vm = new TradeViewModel
            {
                Account = "ACC1",
                AccountType = "Type1",
                BuyQuantity = 100,
                SellQuantity = 50,
                BuyPrice = 10,
                SellPrice = 12,
                TradeDate = DateTime.UtcNow,
                TradeSecurity = "Sec1",
                TradeStatus = "Open",
                Trader = "Trader1"
            };
            var created = new Trade { Id = 1, Account = vm.Account };

            _serviceMock.Setup(s => s.SaveTrade(vm)).ReturnsAsync(created);

            var result = await _controller.Create(vm);

            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Which;
            createdResult.ActionName.Should().Be(nameof(_controller.GetById));
            createdResult.RouteValues!["id"].Should().Be(created.Id);
            createdResult.Value.Should().Be(created);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsBadRequest()
        {
            var vm = new TradeViewModel();
            _controller.ModelState.AddModelError("Account", "Required");

            var result = await _controller.Create(vm);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_ValidId_ReturnsOk_WithUpdatedEntity()
        {
            var vm = new TradeViewModel
            {
                Id = 1,
                Account = "ACC1",
                AccountType = "Type1",
                TradeSecurity = "Sec1",
                TradeStatus = "Open",
                Trader = "Trader1"
            };
            var existing = new Trade { Id = 1, Account = "Old" };
            var updated = new Trade { Id = 1, Account = "ACC1" };

            _serviceMock.Setup(s => s.GetTradeById(1)).ReturnsAsync(existing);
            _serviceMock.Setup(s => s.UpdateTrade(vm)).ReturnsAsync(updated);

            var result = await _controller.Update(1, vm);

            var okResult = result.Should().BeOfType<OkObjectResult>().Which;
            okResult.Value.Should().Be(updated);
            _serviceMock.Verify(s => s.UpdateTrade(vm), Times.Once);
        }

        [Fact]
        public async Task Update_IdMismatch_ReturnsBadRequest()
        {
            var vm = new TradeViewModel { Id = 2 };

            var result = await _controller.Update(1, vm);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_NotFound_ReturnsNotFound()
        {
            var vm = new TradeViewModel { Id = 1 };
            _serviceMock.Setup(s => s.GetTradeById(1)).ReturnsAsync((Trade?)null);

            var result = await _controller.Update(1, vm);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Update_InvalidModel_ReturnsBadRequest()
        {
            var vm = new TradeViewModel { Id = 1 };
            _controller.ModelState.AddModelError("Account", "Required");

            var result = await _controller.Update(1, vm);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnsNoContent()
        {
            var existing = new Trade { Id = 1 };
            _serviceMock.Setup(s => s.GetTradeById(1)).ReturnsAsync(existing);
            _serviceMock.Setup(s => s.DeleteTrade(1)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(1);

            result.Should().BeOfType<NoContentResult>();
            _serviceMock.Verify(s => s.DeleteTrade(1), Times.Once);
        }

        [Fact]
        public async Task Delete_NotFound_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetTradeById(1)).ReturnsAsync((Trade?)null);

            var result = await _controller.Delete(1);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
