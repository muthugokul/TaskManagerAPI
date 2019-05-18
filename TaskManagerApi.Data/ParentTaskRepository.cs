using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManagerApi.Data.Interface;
using TaskManagerApi.Model;

namespace TaskManagerApi.Data
{
    public class ParentTaskRepository : IRepository<Model.ParentTask>
    {
        private readonly TaskManagerDbContext dbContext;

        public ParentTaskRepository(TaskManagerDbContext context)
        {
            this.dbContext = context;
        }

        public Task<int> Create(ParentTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> EndTask(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ParentTask> Get(int id)
        {
            return await this.dbContext.ParentTasks.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Model.ParentTask>> GetAll()
        {
            return await this.dbContext.ParentTasks.ToListAsync();
        }

        public Task<int> Update(ParentTask entity)
        {
            throw new NotImplementedException();
        }
    }
}
