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

        public async Task<int> Update(Model.Task task)
        {
            var entity = await Get(task.Id);
            entity.Name = task.Name;
            entity.Priority = task.Priority;
            entity.StartDate = task.StartDate;
            entity.EndDate = task.EndDate;
            entity.ParentTask = task.ParentTask;

            if (task.ParentTask != null)
            {
                if (task.ParentTask.Id == 0)
                {
                    this.dbContext.ParentTasks.Add(task.ParentTask);
                    await this.dbContext.SaveChangesAsync();
                    entity.ParentTask = task.ParentTask;
                }
                else
                {
                    entity.ParentTask = this.dbContext.ParentTasks.FirstOrDefault(x => x.Id == task.ParentTask.Id);
                }
            } 

            this.dbContext.Update(entity);
            return await this.dbContext.SaveChangesAsync();
        }

        public async Task<int> EndTask(int id)
        {
            var task = await this.Get(id);
            task.EndDate = DateTime.Now;
            task.IsComplete = true;

            return await this.dbContext.SaveChangesAsync();
        }
    }
}
