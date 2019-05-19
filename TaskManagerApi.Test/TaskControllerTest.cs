
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
using TaskManagerApi.Model.Contracts;

namespace TaskManagerApi.Test
{
    public class TaskControllerTest : IClassFixture<LoggerFixture<TasksController>>
    {
        #region Private Fields

        private readonly LoggerFixture<TasksController> fixture;
        private Mock<IService<Model.Task>> mockTaskService;
        private TasksController controller;

        #endregion

        #region Constructor

        public TaskControllerTest(LoggerFixture<TasksController> loggerFixture)
        {
            fixture = loggerFixture;

            mockTaskService = new Mock<IService<Model.Task>>();
            controller = new TasksController(mockTaskService.Object, fixture.Logger);
        }

        #endregion

        #region GetAll

        [Fact]
        public async Task GetAll_Returns_AllTasks()
        {
            // Arrange
            mockTaskService.Setup(service => service.GetAll()).Returns(Task.FromResult<IEnumerable<Model.Task>>(TaskFixture.Tasks()));

            // Act
            var results = await controller.GetAll();

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(results);
            var tasks = Assert.IsAssignableFrom<IEnumerable<Model.Task>>(objectResult.Value);
            Assert.Equal(4, tasks.Count());
        }

        [Fact]
        public async Task GetAll_Throws_InternalServerError()
        {
            // Arrange
            mockTaskService.Setup(service => service.GetAll()).Throws(new Exception());

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result as ObjectResult).StatusCode);
        }

        #endregion

        #region Get

        [Fact]
        public async Task Get_Returns_ExpectedTask()
        {
            // Arrange
            var task = TaskFixture.Tasks().FirstOrDefault(x => x.Id == 1);
            mockTaskService.Setup(service => service.Get(1)).Returns(Task.FromResult<Model.Task>(task));

            // Act
            var result = await controller.Get(1);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var actualTask = Assert.IsAssignableFrom<Model.Task>(objectResult.Value);
            Assert.Equal(task.Id, actualTask.Id);
            Assert.NotNull(actualTask.ParentTask);
            Assert.Equal(task.ParentTask, actualTask.ParentTask);
        }

        [Fact]
        public async Task Get_Returns_NotFound_GivenInvalidId()
        {
            // Arrange
            mockTaskService.Setup(service => service.Get(10)).Returns(Task.FromResult<Model.Task>(null));

            // Act
            var result = await controller.Get(10);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_Throws_InternalServerError()
        {
            // Arrange
            mockTaskService.Setup(service => service.Get(10)).Throws(new Exception());

            // Act
            var result = await controller.Get(10);

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result as ObjectResult).StatusCode);
        }

        #endregion

        #region Create

        [Fact]
        public async Task Create_Returns_BadRequest_When_ModelStateIsInvalid()
        {
            // Arrange
            controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await controller.Create(new CreateTask());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_Returns_SuccessResponse()
        {
            // Arrange
            var createTask = new CreateTask { Name = "New Task", Priority = 1, StartDate = new DateTime() };
            mockTaskService.Setup(service => service.Create(It.IsAny<Model.Task>())).Returns(Task.FromResult<int>(1));

            // Act
            var result = await controller.Create(new CreateTask());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Create_Throws_InternalServerError()
        {
            // Arrange
            mockTaskService.Setup(service => service.Create(It.IsAny<Model.Task>())).Throws(new Exception());

            // Act
            var result = await controller.Create(new CreateTask());

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result as ObjectResult).StatusCode);
        }

        #endregion

        #region Put

        [Fact]
        public async Task Update_Returns_BadRequest_WhenIdIsInvalid()
        {
            // Arrange
            mockTaskService.Setup(service => service.Get(It.IsAny<int>())).Returns(Task.FromResult<Model.Task>(null));

            // Act
            var result = await controller.Update(100, new UpdateTask { Id = 1});

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Update_Returns_BadRequest_When_ModelStateIsInvalid()
        {
            // Arrange
            controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await controller.Update(1, new UpdateTask { Id = 1});

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task Update_Returns_NotFound_WhenIdIsInvalid()
        {
            // Arrange
            var taskToUpdate = TaskFixture.Tasks().First();
            mockTaskService.Setup(service => service.Get(It.IsAny<int>())).Returns(Task.FromResult<Model.Task>(null));

            // Act
            var result = await controller.Update(taskToUpdate.Id, new UpdateTask { Id = taskToUpdate.Id });

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task Update_Returns_NoContent_When_TaskUpdated()
        {
            // Arrange
            var taskToUpdate = TaskFixture.Tasks().First();
            mockTaskService.Setup(service => service.Get(It.IsAny<int>())).Returns(Task.FromResult<Model.Task>(taskToUpdate));

            // Act
            var result = await controller.Update(taskToUpdate.Id, new UpdateTask { Id = taskToUpdate.Id });

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_Throws_InternalServerError()
        {
            // Arrange
            var taskToUpdate = TaskFixture.Tasks().First();
            mockTaskService.Setup(service => service.Get(It.IsAny<int>())).Throws(new Exception());

            // Act
            var result = await controller.Update(taskToUpdate.Id, new UpdateTask { Id = taskToUpdate.Id });

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result as ObjectResult).StatusCode);
        }

        #endregion

        #region EndTask

        [Fact]
        public async Task EndTask_Returns_BadRequest_When_ModelStateIsInvalid()
        {
            // Arrange
            controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await controller.EndTask(1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task EndTask_Returns_NotFound_WhenIdIsInvalid()
        {
            // Arrange
            var taskToUpdate = TaskFixture.Tasks().First();
            mockTaskService.Setup(service => service.Get(It.IsAny<int>())).Returns(Task.FromResult<Model.Task>(null));

            // Act
            var result = await controller.EndTask(100);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task EndTask_Returns_NoContent_When_TaskUpdated()
        {
            // Arrange
            var taskToUpdate = TaskFixture.Tasks().First();
            mockTaskService.Setup(service => service.Get(It.IsAny<int>())).Returns(Task.FromResult<Model.Task>(taskToUpdate));

            // Act
            var result = await controller.EndTask(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task EndTask_Throws_InternalServerError()
        {
            // Arrange
            var taskToUpdate = TaskFixture.Tasks().First();
            mockTaskService.Setup(service => service.Get(It.IsAny<int>())).Throws(new Exception());

            // Act
            var result = await controller.EndTask(2);

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result as ObjectResult).StatusCode);
        }

        #endregion
    }
}