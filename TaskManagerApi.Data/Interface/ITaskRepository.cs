using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerApi.Data.Interface
{
    public interface ITaskRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void Create(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void End(TEntity entity);
    }
}
