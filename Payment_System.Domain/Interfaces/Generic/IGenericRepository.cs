using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Payment_System.Domain.Interfaces.Generic
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        Task AddAsync(TEntity model);
        Task<TEntity> GetByParam(Expression<Func<TEntity, bool>> filter);
        Task Update(TEntity model);
    }
}
