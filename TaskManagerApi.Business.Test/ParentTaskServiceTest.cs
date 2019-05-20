
using Moq;
using System;
using System.Threading.Tasks;
using TaskManagerApi.Data;
using TaskManagerApi.Data.Interface;
using Xunit;

namespace TaskManagerApi.Business.Test
{
    public class ParentTaskServiceTest
    {
        #region Private Fields

        private Mock<IRepository<Model.ParentTask>> mockRepository;
        private ParentTaskService parentTaskService;
        
        #endregion

        #region Constructor

        public ParentTaskServiceTest()
        {
            mockRepository = new Mock<IRepository<Model.ParentTask>>();
            parentTaskService = new ParentTaskService(mockRepository.Object);
        }

        #endregion

        #region GetAll

        [Fact]
        public async Task GetAll_Calls_TaskRepository_GetAll_Once()
        {
            // Arrange && Act
            var result = await parentTaskService.GetAll();

            // Assert
            mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        #endregion

        #region Get

        [Fact]
        public async Task Get_Throws_NotImplementedException()
        {
            // Act && Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => parentTaskService.Get(1));
        }

        #endregion

        #region Create

        [Fact]
        public async Task Create_Throws_NotImplementedException()
        {
            // Act && Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => parentTaskService.Create(new Model.ParentTask()));
        }

        #endregion

        #region Update

        [Fact]
        public async Task Update_Throws_NotImplementedException()
        {
            // Act && Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => parentTaskService.Update(new Model.ParentTask()));
        }

        #endregion

        #region EndTask

        [Fact]
        public async Task EndTask_Throws_NotImplementedException()
        {
            // Act && Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => parentTaskService.EndTask(1));
        }

        #endregion
    }
}