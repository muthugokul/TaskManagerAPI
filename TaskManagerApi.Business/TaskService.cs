using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerApi.Business.Interface;
using TaskManagerApi.Data.Interface;

namespace TaskManagerApi.Business
{
    public class TaskService : IService<Model.Task>
    {
        private readonly IRepository<Model.Task> taskRepository;
        private readonly IRepository<Model.ParentTask> parentTaskRepository;

        public TaskService(IRepository<Model.Task> taskRepository, IRepository<Model.ParentTask> parentTaskRepository)
        {
            this.taskRepository = taskRepository;
            this.parentTaskRepository = parentTaskRepository;
        }

        public async Task<int> Create(Model.Task entity)
        {
            var parentTask = entity.ParentTask;
            if (parentTask != null && parentTask.Id > 0)
            {
                parentTask = await this.parentTaskRepository.Get(entity.ParentTask.Id);
                entity.ParentTask = parentTask;
            }

            return await this.taskRepository.Create(entity);
        }

        public async Task<int> EndTask(int id)
        {
            return await this.taskRepository.EndTask(id);
        }

        public async Task<Model.Task> Get(int id)
        {
            return await this.taskRepository.Get(id);
        }

        public async Task<IEnumerable<Model.Task>> GetAll()
        {
            return await this.taskRepository.GetAll();
        }

        public async Task<int> Update(Model.Task entity)
        {
            return await this.taskRepository.Update(entity);
        }
    }
}
