using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerApi.Data.Test.Infrastructure;
using Xunit;

namespace TaskManagerApi.Data.Test
{
    public class ParentTaskRepositoryTest : TaskManagerDbTestBase
    {
        #region GetAll

        [Fact]
        public async Task GetAll_Returns_CorrectType()
        {
            // Arrange
            var repository = new ParentTaskRepository(dbContext);

            // Act
            var results = await repository.GetAll();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Model.ParentTask>>(results);
        }

        [Fact]
        public async Task GetAll_Returns_AllParentTasks()
        {
            // Arrange
            var repository = new ParentTaskRepository(dbContext);

            // Act
            var results = await repository.GetAll();

            // Assert
            Assert.Equal(2, results.Count());
        }

        #endregion

        #region Get

        [Fact]
        public async Task Get_Returns_CorrectType()
        {
            // Arrange
            var repository = new ParentTaskRepository(dbContext);

            // Act
            var result = await repository.Get(1);

            // Assert
            Assert.IsAssignableFrom<Model.ParentTask>(result);
        }

        [Fact]
        public async Task Get_Returns_ExpectedParentTask()
        {
            // Arrange
            var repository = new ParentTaskRepository(dbContext);

            // Act
            var result = await repository.Get(1);

            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Parent Task 1", result.Name);
        }

        [Fact]
        public async Task Get_Returns_Null()
        {
            // Arrange
            var repository = new ParentTaskRepository(dbContext);

            // Act
            var result = await repository.Get(3);

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region Create

        [Fact]
        public async Task Create_Throws_NotImplementedException()
        {
            // Arrange
            var repository = new ParentTaskRepository(dbContext);

            // Act && Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => repository.Create(new Model.ParentTask { Id = 5, Name = "Parent Task 5" }));
        }

        #endregion

        #region Update

        [Fact]
        public async Task Update_Throws_NotImplementedException()
        {
            // Arrange
            var repository = new ParentTaskRepository(dbContext);

            // Act && Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => repository.Update(new Model.ParentTask { Id = 1, Name = "Parent Task 1 Update" }));
        }

        #endregion

        #region EndTask

        [Fact]
        public async Task EndTask_Throws_NotImplementedException()
        {
            // Arrange
            var repository = new ParentTaskRepository(dbContext);

            // Act && Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => repository.EndTask(1));
        }

        #endregion
    }
}