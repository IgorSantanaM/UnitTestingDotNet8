﻿
using AutoMapper;
using EmployeeManagement.Controllers;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmployeeManagement.Test
{
    public class StatisticsControllerTests
    {
        [Fact]
        public void Index_InputFromHttpConnectionFeature_MustReturnInputtedIps()
        {
            // Arrange
            var localIpAddress = System.Net.IPAddress .Parse("111.111.111.111");
            var localPort = 5000;
            var remoteIpAddress = System.Net.IPAddress.Parse("222.222.222.222");
            var remotePort = 8080;

            var featureCollectionMock = new Mock<IFeatureCollection>();
            featureCollectionMock.Setup(e => e.Get<IHttpConnectionFeature>())
               .Returns(new HttpConnectionFeature
               {
                   LocalIpAddress = localIpAddress,
                   LocalPort = localPort,
                   RemoteIpAddress = remoteIpAddress,
                   RemotePort = remotePort
               });

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(e => e.Features)
                .Returns(featureCollectionMock.Object);

            var mapperConfiguration = new MapperConfiguration(
              cfg => cfg.AddProfile<MapperProfiles.StatisticsProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var statisticsController = new StatisticsController(mapper);

            statisticsController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContextMock.Object
            };

            // Act
            var result = statisticsController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsType<StatisticsViewModel>(
                viewResult.Model);
            Assert.Equal(localIpAddress.ToString(), viewModel.LocalIpAddress);
            Assert.Equal(localPort, viewModel.LocalPort);
            Assert.Equal(remoteIpAddress.ToString(), viewModel.RemoteIpAddress);
            Assert.Equal(remotePort, viewModel.RemotePort);
        }
    }
}
