using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerApi.Data.Test.Infrastructure
{
    public class TaskManagerDbTestBase : IDisposable
    {
        protected readonly TaskManagerDbContext dbContext;

        public TaskManagerDbTestBase()
        {
            var options = new DbContextOptionsBuilder<TaskManagerDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new TaskManagerDbContext(options);

            dbContext.Database.EnsureCreated();

            TaskManagerDbInitializer.Initialize(dbContext);
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();

            dbContext.Dispose();
        }
    }
}
