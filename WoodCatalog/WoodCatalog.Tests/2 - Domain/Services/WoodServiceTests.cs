using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System.Net;
using WoodCatalog.API.Controllers;
using WoodCatalog.Domain.Models;
using WoodCatalog.Domain.Repositories.Interfaces;
using WoodCatalog.Domain.Services;
using WoodCatalog.Domain.Services.Interfaces;
using WoodCatalog.Tests.Fakers;
using Xunit;

namespace WoodCatalog.Tests.Domain.Services
{
    public class WoodServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly WoodService _woodService;

        public WoodServiceTests()
        {
            _mocker = new AutoMocker();
            _woodService = _mocker.CreateInstance<WoodService>();
        }

        [Fact]
        public void AddWood_MustExecuteWithSuccess()
        {
            // Arrange
            Wood wood = WoodTestsFaker.GenerateRandom().Generate();
            _mocker.GetMock<IWoodRepository>().Setup(x => x.Add(wood));

            // Act
             _woodService.AddWood(wood);

            // assert
            _mocker.GetMock<IWoodRepository>().Verify(x => x.Add(wood), Times.Once);
        }

        [Fact]
        public void DeleteWood_MustExecuteWithSuccess()
        {
            // Arrange
            Wood wood = WoodTestsFaker.GenerateRandom().Generate();
            _mocker.GetMock<IWoodRepository>().Setup(x => x.Delete(wood.Id)).Returns(wood);

            // Act
            _woodService.DeleteWood(wood.Id);

            // assert
            _mocker.GetMock<IWoodRepository>().Verify(x => x.Delete(wood.Id), Times.Once);
        }

        [Fact]
        public void GetAllWoods_MustExecuteWithSuccess()
        {
            // Arrange
            List<Wood> woods = WoodTestsFaker.GenerateRandom().Generate(5);
            _mocker.GetMock<IWoodRepository>().Setup(x => x.GetAll()).Returns(woods);

            // Act
            IEnumerable<Wood> woodsResult = _woodService.GetAllWoods();

            // assert
            Assert.NotNull(woodsResult);
            Assert.NotEmpty(woodsResult);
            _mocker.GetMock<IWoodRepository>().Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void GetByIdWood_MustExecuteWithSuccess()
        {
            // Arrange
            Wood wood = WoodTestsFaker.GenerateRandom().Generate();
            _mocker.GetMock<IWoodRepository>().Setup(x => x.GetById(wood.Id)).Returns(wood);

            // Act
            Wood? woodResult = _woodService.GetWoodById(wood.Id);

            // assert
            Assert.NotNull(woodResult);
            Assert.Equal(wood.Id, woodResult.Id);
            _mocker.GetMock<IWoodRepository>().Verify(x => x.GetById(wood.Id), Times.Once);
        }

        [Fact]
        public void UpdateWood_MustExecuteWithSuccess()
        {
            // Arrange
            Wood wood = WoodTestsFaker.GenerateRandom().Generate();
            _mocker.GetMock<IWoodRepository>().Setup(x => x.Update(wood)).Returns(wood);

            // Act
            Wood? woodResult = _woodService.UpdateWood(wood);

            // assert
            Assert.NotNull(woodResult);
            Assert.Equal(wood.Id, woodResult.Id);
            _mocker.GetMock<IWoodRepository>().Verify(x => x.Update(wood), Times.Once);
        }
    }
}