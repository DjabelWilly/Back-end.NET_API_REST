using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Application.Controllers;

namespace P7CreateRestApi.Tests
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _controller = new HomeController();
        }

        [Fact]
        public void Get_ReturnsOk()
        {
            // Act
            var result = _controller.Get();

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void Admin_ReturnsOk()
        {
            // Act
            var result = _controller.Admin();

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
