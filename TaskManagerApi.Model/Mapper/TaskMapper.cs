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

        public static Model.Task Map(Model.Task entity, UpdateTask updateTask)
        {
            entity.Id = updateTask.Id;
            entity.Name = updateTask.Name;
            entity.Priority = updateTask.Priority;
            entity.StartDate = updateTask.StartDate;
            entity.EndDate = updateTask.EndDate;
        
            if (updateTask.ParentTask != null)
            {
                entity.ParentTask = ParentTaskMapper.Map(entity.ParentTask, updateTask.ParentTask);
            } else
            {
                entity.ParentTask = null;
            }

            return entity;
        }
    }
}
