using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerApi.Test.Fixtures
{
    public static class TaskFixture
    {
        public static IList<Model.Task> Tasks()
        {
            return new List<Model.Task>
            {
                new Model.Task { Id = 1, Name = "Task 1", Priority = 1, StartDate = new DateTime(), ParentTask = new Model.ParentTask { Id = 1, Name = "Parent Task 1" } },
                new Model.Task { Id = 2, Name = "Task 2", Priority = 1, StartDate = new DateTime(), ParentTask = new Model.ParentTask { Id = 2, Name = "Parent Task 2" } },
                new Model.Task { Id = 3, Name = "Task 3", Priority = 1, StartDate = new DateTime(), ParentTask = new Model.ParentTask { Id = 1, Name = "Parent Task 1" } },
                new Model.Task { Id = 4, Name = "Task 4", Priority = 1, StartDate = new DateTime() }
            };
        }
    }
}
