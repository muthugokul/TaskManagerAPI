using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApi.Data.Interface
{
    public interface ITaskRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        Task<int> Create(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void End(TEntity entity);
    }
}
