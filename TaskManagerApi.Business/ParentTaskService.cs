using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerApi.Business.Interface;
using TaskManagerApi.Data.Interface;
using TaskManagerApi.Model;

namespace TaskManagerApi.Business
{
    public class ParentTaskService : IService<ParentTask>
    {
        private readonly IRepository<Model.ParentTask> parentTaskRepository;

        public ParentTaskService(IRepository<Model.ParentTask> parentTaskRepository)
        {
            this.parentTaskRepository = parentTaskRepository;
        }

        public Task<int> Create(ParentTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> EndTask(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ParentTask> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ParentTask>> GetAll()
        {
            return await this.parentTaskRepository.GetAll();
        }

        public Task<int> Update(ParentTask entity)
        {
            throw new NotImplementedException();
        }
    }
}
