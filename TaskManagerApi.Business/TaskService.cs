using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerApi.Business.Interface;
using TaskManagerApi.Data.Interface;

namespace TaskManagerApi.Business
{
    public class TaskService : IService<Model.Task>
    {
        private IRepository<Model.Task> taskRepository;

        public TaskService(IRepository<Model.Task> taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public Task<int> Create(Model.Task entity)
        {
            throw new NotImplementedException();
        }

        public void EndTask(Model.Task entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Model.Task> Get(int id)
        {
            return await this.taskRepository.Get(id);
        }

        public async Task<IEnumerable<Model.Task>> GetAll()
        {
            return await this.taskRepository.GetAll();
        }

        public void Update(Model.Task dbEntity, Model.Task entity)
        {
            throw new NotImplementedException();
        }
    }
}
