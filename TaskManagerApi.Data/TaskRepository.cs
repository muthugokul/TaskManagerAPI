using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagerApi.Data.Interface;
using TaskManagerApi.Model;

namespace TaskManagerApi.Data
{
    public class TaskRepository : ITaskRepository<Task>
    {
        readonly TaskManagerDbContext DbContext;

        public TaskRepository(TaskManagerDbContext context)
        {
            DbContext = context;
        }

        public IEnumerable<Task> GetAll()
        {
            return DbContext.Tasks.ToList();
        }

        public Task Get(int id)
        {
            return DbContext.Tasks
                  .FirstOrDefault(e => e.Id == id);
        }

        public void Create(Task entity)
        {
            DbContext.Tasks.Add(entity);
            DbContext.SaveChanges();
        }

        public void Update(Task task, Task entity)
        {
            task.Id = entity.Id;
            task.Name = entity.Name;
            task.Priority = entity.Priority;

            DbContext.SaveChanges();
        }

        public void End(Task task)
        {
            task.IsComplete = true;
            DbContext.SaveChanges();
        }
    }
}
