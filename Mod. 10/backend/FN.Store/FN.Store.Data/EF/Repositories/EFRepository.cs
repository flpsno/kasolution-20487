using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FN.Store.Data.EF.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        protected StoreDataContext _ctx;
        private readonly DbSet<TEntity> _dbSet;

        public EFRepository(StoreDataContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
           
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Edit(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.ToList();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
