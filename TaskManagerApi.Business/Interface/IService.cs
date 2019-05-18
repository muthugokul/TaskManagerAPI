using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApi.Business.Interface
{
    public interface IService<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<int> Create(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void EndTask(TEntity entity);
    }
}
