using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerApi.Data.Test.Infrastructure;
using Xunit;

namespace TaskManagerApi.Data.Test
{
    public class TaskRepositoryTest : TaskManagerDbTestBase
    {
        #region GetAll

        [Fact]
        public async Task GetAll_Returns_CorrectType()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);

            // Act
            var results = await repository.GetAll();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<Model.Task>>(results);
        }

        [Fact]
        public async Task GetAll_Returns_AllTasks()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);

            // Act
            var results = await repository.GetAll();

            // Assert
            Assert.Equal(4, results.Count());
        }

        [Fact]
        public async Task GetAll_Include_ParentTask()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);

            // Act
            var results = await repository.GetAll();
            var resultList = results.ToList();
            
            // Assert
            Assert.NotNull(resultList[0].ParentTask);
            Assert.NotNull(resultList[1].ParentTask);
            Assert.NotNull(resultList[2].ParentTask);
            Assert.Null(resultList[3].ParentTask);
        }

        #endregion

        #region Get

        [Fact]
        public async Task Get_Returns_CorrectType()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);

            // Act
            var result = await repository.Get(1);

            // Assert
            Assert.IsAssignableFrom<Model.Task>(result);
        }

        [Fact]
        public async Task Get_Returns_ExpectedTask()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);

            // Act
            var result = await repository.Get(2);

            // Assert
            Assert.Equal(2, result.Id);
            Assert.Equal("Task 2", result.Name);
        }

        [Fact]
        public async Task Get_Include_ParentTask()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);

            // Act
            var result = await repository.Get(1);

            // Assert
            Assert.NotNull(result.ParentTask);
            Assert.Equal(1, result.ParentTask.Id);
            Assert.Equal("Parent Task 1", result.ParentTask.Name);
        }

        #endregion

        #region Create

        [Fact]
        public async Task Create_Inserts_NewTask_With_NoParentTask()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);
            var parentTaskRepository = new ParentTaskRepository(dbContext);

            // Act
            var result = await repository.Create(new Model.Task { Id= 5, Name = "Task 5", Priority = 1, StartDate = new DateTime() });
            var actualTasks = await repository.GetAll();
            var actualParentTasks = await parentTaskRepository.GetAll();

            // Assert
            Assert.Equal(5, actualTasks.Count());
            Assert.Equal(2, actualParentTasks.Count());
        }

        [Fact]
        public async Task Create_Does_Not_Inserts_ParentTask_If_Aleardy_Exists()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);
            var parentTaskRepository = new ParentTaskRepository(dbContext);

            // Act
            var result = await repository.Create(new Model.Task { Id = 7, Name = "Task 7", Priority = 1, StartDate = new DateTime(), ParentTask = new Model.ParentTask { Id = 1, Name = "Parent Task 3" } });
            var actualTasks = await repository.GetAll();
            var actualParentTasks = await parentTaskRepository.GetAll();

            // Assert
            Assert.Equal(5, actualTasks.Count());
            Assert.Equal(2, actualParentTasks.Count());
        }

        [Fact]
        public async Task Create_Inserts_NewTask_And_ParentTask()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);
            var parentTaskRepository = new ParentTaskRepository(dbContext);

            // Act
            var result = await repository.Create(new Model.Task { Id = 6, Name = "Task 6", Priority = 1, StartDate = new DateTime(), ParentTask = new Model.ParentTask { Id = 3, Name = "Parent Task 3" } });
            var actualTasks = await repository.GetAll();
            var actualParentTasks = await parentTaskRepository.GetAll();

            // Assert
            Assert.Equal(5, actualTasks.Count());
            Assert.Equal(3, actualParentTasks.Count());
        }

        #endregion

        #region Update

        [Fact]
        public async Task Update_Saves_Task_With_WithChanges()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);
            var parentTaskRepository = new ParentTaskRepository(dbContext);

            var task = await repository.Get(1);
            task.Name = "New Task Name";
            task.Priority = 2;

            // Act
            var result = await repository.Update(task);
            var actualTask = await repository.Get(1);
            var actualParentTasks = await parentTaskRepository.GetAll();

            // Assert
            Assert.Equal(task.Id, actualTask.Id);
            Assert.Equal(task.Name, actualTask.Name);
            Assert.Equal(task.Priority, actualTask.Priority);
            Assert.Equal(2, actualParentTasks.Count());
        }

        [Fact]
        public async Task Update_Does_Not_Inserts_ParentTask_If_Aleardy_Exists()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);
            var parentTaskRepository = new ParentTaskRepository(dbContext);

            var task = await repository.Get(2);
            task.Name = "New Task Name";

            // Act
            var result = await repository.Update(task);
            var actualTask = await repository.Get(2);
            var actualParentTasks = await parentTaskRepository.GetAll();

            // Assert
            Assert.Equal(task.Id, actualTask.Id);
            Assert.Equal(task.Name, actualTask.Name);
            Assert.Equal(2, actualParentTasks.Count());
        }

        [Fact]
        public async Task Update_Inserts_ParentTask()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);
            var parentTaskRepository = new ParentTaskRepository(dbContext);

            var task = await repository.Get(4);
            task.ParentTask = new Model.ParentTask { Id = 10, Name = "Parent task 10" };

            // Act
            var result = await repository.Update(task);
            var actualTask = await repository.Get(4);
            var actualParentTasks = await parentTaskRepository.GetAll();

            // Assert
            Assert.Equal(task.Id, actualTask.Id);
            Assert.Equal(task.ParentTask.Id, actualTask.ParentTask.Id);
            Assert.Equal(task.ParentTask.Name, actualTask.ParentTask.Name);
            Assert.Equal(3, actualParentTasks.Count());
        }

        [Fact]
        public async Task Update_Saves_ParentTask()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);
            var parentTaskRepository = new ParentTaskRepository(dbContext);

            var parentTask = await parentTaskRepository.Get(1);
            var task = await repository.Get(4);
            task.ParentTask = parentTask;

            // Act
            var result = await repository.Update(task);
            var actualTask = await repository.Get(4);
            var actualParentTasks = await parentTaskRepository.GetAll();

            // Assert
            Assert.Equal(task.Id, actualTask.Id);
            Assert.Equal(task.ParentTask.Id, actualTask.ParentTask.Id);
            Assert.Equal(task.ParentTask.Name, actualTask.ParentTask.Name);
            Assert.Equal(2, actualParentTasks.Count());
        }

        #endregion

        #region EndTask

        [Fact]
        public async Task EndTask_Update_IsCompleted_To_True()
        {
            // Arrange
            var repository = new TaskRepository(dbContext);

            // Act
            var result = await repository.EndTask(1);
            var actualTask = await repository.Get(1);

            // Assert
            Assert.True(actualTask.IsComplete);
        }

        #endregion
    }
}
