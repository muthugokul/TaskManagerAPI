using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApi.Data.Interface
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<int> Create(TEntity entity);
        Task<int> Update(TEntity entity);
        Task<int> EndTask(int id);
    }
}
