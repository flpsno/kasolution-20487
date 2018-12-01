using FN.Store.Domain.Contracts.Data;
using System.Threading.Tasks;

namespace FN.Store.Data.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDataContext _ctx;

        public UnitOfWork(StoreDataContext ctx) => _ctx = ctx;

        public async Task CommitAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public Task RollBackAsync()
        {
            return null;
        }
    }
}
