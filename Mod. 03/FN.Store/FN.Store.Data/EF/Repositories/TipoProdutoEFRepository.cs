using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;

namespace FN.Store.Data.EF.Repositories
{
    public class TipoProdutoEFRepository : EFRepository<TipoProduto>, ITipoProdutoRepository
    {
        public TipoProdutoEFRepository(StoreDataContext ctx) : base(ctx) { }
    }
}
