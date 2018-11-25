using FN.Store.Domain.Entities;
using System.Collections.Generic;

namespace FN.Store.Domain.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity: Entity
    {
        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);

        IEnumerable<TEntity> Get();
        TEntity Get(int id);
    }
}
