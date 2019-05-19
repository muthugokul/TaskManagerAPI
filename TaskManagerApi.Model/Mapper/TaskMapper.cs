using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApi.Model.Contracts;

namespace TaskManagerApi.Model.Mapper
{
    public static class TaskMapper
    {
        public static Model.Task Map(CreateTask newTask)
        {
            var task = new Task
            {
                Name = newTask.Name,
                Priority = newTask.Priority,
                StartDate = newTask.StartDate,
                EndDate = newTask.EndDate
            };

            if (newTask.ParentTask != null)
            {
                task.ParentTask = ParentTaskMapper.Map(newTask.ParentTask);
            }

            return task;
        }
    }
}
