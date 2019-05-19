using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagerApi.Model;

namespace TaskManagerApi.Data.Test.Infrastructure
{
    public class TaskManagerDbInitializer
    {
        public static void Initialize(TaskManagerDbContext dbContext)
        {
            if (dbContext.Tasks.Any())
            {
                return;
            }

            Seed(dbContext);
        }

        private static void Seed(TaskManagerDbContext dbContext)
        {
            //var parentTask = GetParentTasks()

            var tasks = GetTasks();

            dbContext.Tasks.AddRange(tasks);
            dbContext.SaveChanges();
        }

        private static IList<ParentTask> GetParenttasks()
        {
            return new List<ParentTask>
            {
                new ParentTask { Id = 1, Name = "Parent Task 1" },
                new ParentTask { Id = 2, Name = "Parent Task 2" },
                new ParentTask { Id = 3, Name = "Parent Task 3" },
            };
        }

        private static IList<Task> GetTasks()
        {
            return new List<Task>
            {
                new Task { Id = 1, Name = "Task 1", Priority = 1, StartDate = new DateTime(), ParentTask = new ParentTask { Id = 1, Name = "Parent Task 1" } },
                new Task { Id = 2, Name = "Task 2", Priority = 1, StartDate = new DateTime(), ParentTask = new ParentTask { Id = 2, Name = "Parent Task 2" } },
                new Task { Id = 3, Name = "Task 3", Priority = 1, StartDate = new DateTime(), ParentTask = new ParentTask { Id = 1, Name = "Parent Task 1" } },
                new Task { Id = 4, Name = "Task 4", Priority = 1, StartDate = new DateTime() }
            };
        }
    }
}
