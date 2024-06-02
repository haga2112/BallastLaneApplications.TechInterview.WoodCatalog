using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using StackExchange.Redis;
using System.Net;
using WoodCatalog.API.Controllers;
using WoodCatalog.Domain.Models;
using WoodCatalog.Domain.Repositories.Interfaces;
using WoodCatalog.Domain.Services;
using WoodCatalog.Domain.Services.Interfaces;
using WoodCatalog.Redis;
using WoodCatalog.Tests.Fakers;
using Xunit;

namespace WoodCatalog.Tests.Domain.Services
{
    public class WoodRepositoryTests
    {
        private readonly AutoMocker _mocker;
        private readonly WoodRepository _woodRepository;

        public WoodRepositoryTests()
        {
            _mocker = new AutoMocker();
            _woodRepository = _mocker.CreateInstance<WoodRepository>();
        }

        [Fact]
        public void Add_MustExecuteWithSuccess()
        {
            // Arrange
            Wood wood = WoodTestsFaker.GenerateRandom().Generate();
            _mocker.GetMock<IWoodRepository>().Setup(x => x.Add(wood));
            var mockMultiplexer = _mocker.GetMock<IConnectionMultiplexer>();
            var mockDatabase = new Mock<IDatabase>();
            mockMultiplexer.Setup(_ => _.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(mockDatabase.Object);

            // Act
            _woodRepository.Add(wood);

            // assert
            //_mocker.GetMock<IWoodRepository>().Verify(x => x.Add(wood), Times.Once);
        }

        //[Fact]
        //public void DeleteWood_MustExecuteWithSuccess()
        //{
        //    // Arrange
        //    Wood wood = WoodTestsFaker.GenerateRandom().Generate();
        //    _mocker.GetMock<IWoodRepository>().Setup(x => x.Delete(wood.Id)).Returns(wood);

        //    // Act
        //    _woodRepository.Delete(wood.Id);

        //    // assert
        //    _mocker.GetMock<IWoodRepository>().Verify(x => x.Delete(wood.Id), Times.Once);
        //}

        //[Fact]
        //public void GetAllWoods_MustExecuteWithSuccess()
        //{
        //    // Arrange
        //    List<Wood> woods = WoodTestsFaker.GenerateRandom().Generate(5);
        //    _mocker.GetMock<IWoodRepository>().Setup(x => x.GetAll()).Returns(woods);

        //    // Act
        //    IEnumerable<Wood> woodsResult = _woodRepository.GetAll();

        //    // assert
        //    Assert.NotNull(woodsResult);
        //    Assert.NotEmpty(woodsResult);
        //    _mocker.GetMock<IWoodRepository>().Verify(x => x.GetAll(), Times.Once);
        //}

        //[Fact]
        //public void GetByIdWood_MustExecuteWithSuccess()
        //{
        //    // Arrange
        //    Wood wood = WoodTestsFaker.GenerateRandom().Generate();
        //    _mocker.GetMock<IWoodRepository>().Setup(x => x.GetById(wood.Id)).Returns(wood);

        //    // Act
        //    Wood? woodResult = _woodRepository.GetById(wood.Id);

        //    // assert
        //    Assert.NotNull(woodResult);
        //    Assert.Equal(wood.Id, woodResult.Id);
        //    _mocker.GetMock<IWoodRepository>().Verify(x => x.GetById(wood.Id), Times.Once);
        //}

        //[Fact]
        //public void UpdateWood_MustExecuteWithSuccess()
        //{
        //    // Arrange
        //    Wood wood = WoodTestsFaker.GenerateRandom().Generate();
        //    _mocker.GetMock<IWoodRepository>().Setup(x => x.Update(wood)).Returns(wood);

        //    // Act
        //    Wood? woodResult = _woodRepository.Update(wood);

        //    // assert
        //    Assert.NotNull(woodResult);
        //    Assert.Equal(wood.Id, woodResult.Id);
        //    _mocker.GetMock<IWoodRepository>().Verify(x => x.Update(wood), Times.Once);
        //}
    }
}