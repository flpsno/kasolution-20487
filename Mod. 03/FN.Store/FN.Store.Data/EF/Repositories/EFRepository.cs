using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

            // Todo: UoW
            _ctx.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);

            _ctx.SaveChanges();
        }

        public void Edit(TEntity entity)
        {
            _dbSet.Update(entity);
            _ctx.SaveChanges();
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.ToList();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }
    }
}
