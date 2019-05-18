using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerApi.Data.Interface;
using TaskManagerApi.Model;

namespace TaskManagerApi.Data
{
    public class TaskRepository : IRepository<Model.Task>
    {
        private readonly TaskManagerDbContext dbContext;

        public TaskRepository(TaskManagerDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<IEnumerable<Model.Task>> GetAll()
        {
            return await this.dbContext.Tasks.Include(p => p.ParentTask).ToListAsync();
        }

        public async Task<Model.Task> Get(int id)
        {
            return await this.dbContext.Tasks.Include(p => p.ParentTask).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<int> Create(Model.Task entity)
        {
            this.dbContext.Tasks.Add(entity);
            return await this.dbContext.SaveChangesAsync();
        }

        public void Update(Model.Task task, Model.Task entity)
        {
            task.Id = entity.Id;
            task.Name = entity.Name;
            task.Priority = entity.Priority;

            this.dbContext.SaveChanges();
        }

        public void EndTask(Model.Task task)
        {
            task.IsComplete = true;
            this.dbContext.SaveChanges();
        }
    }
}
