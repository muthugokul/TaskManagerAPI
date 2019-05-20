
using Moq;
using System;
using System.Threading.Tasks;
using TaskManagerApi.Data;
using TaskManagerApi.Data.Interface;
using Xunit;

namespace TaskManagerApi.Business.Test
{
    public class TaskServiceTest
    {
        #region Private Fields

        private Mock<IRepository<Model.ParentTask>> mockParentTaskRepository;
        private Mock<IRepository<Model.Task>> mockRepository;
        private TaskService taskService;
        
        #endregion

        #region Constructor

        public TaskServiceTest()
        {
            mockRepository = new Mock<IRepository<Model.Task>>();
            mockParentTaskRepository = new Mock<IRepository<Model.ParentTask>>();
            taskService = new TaskService(mockRepository.Object, mockParentTaskRepository.Object);
        }

        #endregion

        #region GetAll

        [Fact]
        public async Task GetAll_Calls_TaskRepository_GetAll_Once()
        {
            // Arrange && Act
            var result = await taskService.GetAll();

            // Assert
            mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        #endregion

        #region Get

        [Fact]
        public async Task Get_Calls_TaskRepository_Get_Once()
        {
            // Arrange && Act
            var result = await taskService.Get(1);

            // Assert
            mockRepository.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
        }

        #endregion

        #region Create

        [Fact]
        public async Task Create_Calls_TaskRepository_Create_Once()
        {
            // Arrange && Act
            var result = await taskService.Create(new Model.Task());

            // Assert
            mockRepository.Verify(r => r.Create(It.IsAny<Model.Task>()), Times.Once);
            mockParentTaskRepository.Verify(r => r.Create(It.IsAny<Model.ParentTask>()), Times.Never);
        }

        [Fact]
        public async Task Create_Calls_ParentTaskRepository_Get_Once()
        {
            // Arrange && Act
            var result = await taskService.Create(new Model.Task { Id = 1, ParentTask = new Model.ParentTask { Id = 1 } });

            // Assert
            mockRepository.Verify(r => r.Create(It.IsAny<Model.Task>()), Times.Once);
            mockParentTaskRepository.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
        }

        #endregion

        #region Update

        [Fact]
        public async Task Update_Calls_TaskRepository_Update_Once()
        {
            // Arrange && Act
            var result = await taskService.Update(new Model.Task());

            // Assert
            mockRepository.Verify(r => r.Update(It.IsAny<Model.Task>()), Times.Once);
            mockParentTaskRepository.Verify(r => r.Update(It.IsAny<Model.ParentTask>()), Times.Never);
        }

        [Fact]
        public async Task Update_Calls_ParentTaskRepository_Get_Once()
        {
            // Arrange && Act
            var result = await taskService.Update(new Model.Task { Id = 1, ParentTask = new Model.ParentTask { Id = 1 } });

            // Assert
            mockRepository.Verify(r => r.Update(It.IsAny<Model.Task>()), Times.Once);
            mockParentTaskRepository.Verify(r => r.Get(It.IsAny<int>()), Times.Once);
        }

        #endregion

        #region EndTask

        [Fact]
        public async Task EndTask_Calls_ParentTaskRepository_EndTask_Once()
        {
            // Arrange && Act
            var result = await taskService.EndTask(1);

            // Assert
            mockRepository.Verify(r => r.EndTask(It.IsAny<int>()), Times.Once);
        }

        #endregion
    }
}