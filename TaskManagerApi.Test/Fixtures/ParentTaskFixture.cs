using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerApi.Test.Fixtures
{
    public static class ParentTaskFixture
    {
        public static IList<Model.ParentTask> ParentTasks()
        {
            return new List<Model.ParentTask>
            {
                new Model.ParentTask { Id = 1, Name = "Parent Task 1" },
                new Model.ParentTask { Id = 2, Name = "Parent Task 2" },
                new Model.ParentTask { Id = 3, Name = "Parent Task 3" }
            };
        }
    }
}
