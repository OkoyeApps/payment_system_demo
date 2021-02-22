using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Payment_System.Domain.DbContexts;
using Payment_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Payment_System.Domain.Interfaces.Generic
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal PaymentDbContext _context;
        internal DbSet<TEntity> _dbSet;
        public GenericRepository(PaymentDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity model)
        {
            _dbSet.Add(model);
           await _context.SaveChangesAsync();
        }
        
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public async Task<TEntity> GetByParam(Expression<Func<TEntity, bool>> filter)
        {
            if (filter is null) throw new ArgumentNullException(nameof(filter));

            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task Update(TEntity model)
        {
            _dbSet.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
        }
    }
}
