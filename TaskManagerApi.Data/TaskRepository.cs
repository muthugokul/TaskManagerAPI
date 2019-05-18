using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerApi.Data.Interface;
using TaskManagerApi.Model;

namespace TaskManagerApi.Data
{
    public class TaskRepository : ITaskRepository<Model.Task>
    {
        readonly TaskManagerDbContext DbContext;

        public TaskRepository(TaskManagerDbContext context)
        {
            DbContext = context;
        }

        public IEnumerable<Model.Task> GetAll()
        {
            return DbContext.Tasks.ToList();
        }

        public Model.Task Get(int id)
        {
            return DbContext.Tasks
                  .FirstOrDefault(e => e.Id == id);
        }

        public async Task<int> Create(Model.Task entity)
        {
            DbContext.Tasks.Add(entity);
            return await DbContext.SaveChangesAsync();
        }

        public void Update(Model.Task task, Model.Task entity)
        {
            task.Id = entity.Id;
            task.Name = entity.Name;
            task.Priority = entity.Priority;

            DbContext.SaveChanges();
        }

        public void End(Model.Task task)
        {
            task.IsComplete = true;
            DbContext.SaveChanges();
        }
    }
}
