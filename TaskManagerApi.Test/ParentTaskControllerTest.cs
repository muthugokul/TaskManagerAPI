
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using TaskManagerApi.Business.Interface;
using TaskManagerApi.Controllers;
using TaskManagerApi.Test.Fixtures;
using Xunit;

namespace TaskManagerApi.Test
{
    public class ParentTaskControllerTest : IClassFixture<LoggerFixture<ParentTasksController>>
    {
        #region Private Fields

        private readonly LoggerFixture<ParentTasksController> fixture;
        private Mock<IService<Model.ParentTask>> mockParentTaskService;
        private ParentTasksController controller;

        #endregion

        #region Constructor

        public ParentTaskControllerTest(LoggerFixture<ParentTasksController> loggerFixture)
        {
            fixture = loggerFixture;

            mockParentTaskService = new Mock<IService<Model.ParentTask>>();
            controller = new ParentTasksController(mockParentTaskService.Object, fixture.Logger);
        }

        #endregion

        #region GetAll

        [Fact]
        public async Task GetAll_Returns_AllParentTasks()
        {
            // Arrange
            mockParentTaskService.Setup(service => service.GetAll()).Returns(Task.FromResult<IEnumerable<Model.ParentTask>>(ParentTaskFixture.ParentTasks()));

            // Act
            var results = await controller.GetAll();

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(results);
            var parentTasks = Assert.IsAssignableFrom<IEnumerable<Model.ParentTask>>(objectResult.Value);
            Assert.Equal(3, parentTasks.Count());
        }

        [Fact]
        public async Task GetAll_Throws_InternalServerError()
        {
            // Arrange
            mockParentTaskService.Setup(service => service.GetAll()).Throws(new Exception());

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result as ObjectResult).StatusCode);
        }

        #endregion
    }
}