using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System.Net;
using WoodCatalog.API.Controllers;
using WoodCatalog.Domain.Models;
using WoodCatalog.Domain.Services.Interfaces;
using WoodCatalog.Tests.Fakers;
using static System.Formats.Asn1.AsnWriter;

namespace WoodCatalog.Tests.Application.Controllers
{
    public class WoodControllerTests
    {
        private readonly AutoMocker _mocker;
        private readonly WoodController _woodController;

        public WoodControllerTests()
        {
            _mocker = new AutoMocker();
            _woodController = _mocker.CreateInstance<WoodController>();
            _woodController.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new System.Security.Claims.ClaimsPrincipal()
                }
            };
        }

        [Fact]
        public void GetWoodById_MustExecuteWithSuccess()
        {
            // Arrange
            Wood wood = WoodTestsFaker.GenerateRandom().Generate();
            _mocker.GetMock<IWoodService>().Setup(x => x.GetWoodById(wood.Id)).Returns(wood);

            // Act
            IActionResult actionResult = _woodController.GetWoodById(wood.Id);

            // assert
            var okResult = (OkObjectResult)actionResult;
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<Wood?>(okResult.Value);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            _mocker.GetMock<IWoodService>().Verify(x => x.GetWoodById(wood.Id), Times.Once);
        }

        [Fact]
        public void GetAllWoods_MustExecuteWithSuccess()
        {
            // Arrange
            List<Wood> wood = WoodTestsFaker.GenerateRandom().Generate(5);
            _mocker.GetMock<IWoodService>().Setup(x => x.GetAllWoods()).Returns(wood);

            // Act
            IActionResult actionResult = _woodController.GetAllWoods();

            // assert
            var okResult = (OkObjectResult)actionResult;
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<List<Wood>>(okResult.Value);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            _mocker.GetMock<IWoodService>().Verify(x => x.GetAllWoods(), Times.Once);
        }
    }
}