using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FN.Store.Data.EF.Repositories
{
    public class ProdutoEFRepository : EFRepository<Produto>, IProdutoRepository
    {
        public ProdutoEFRepository(StoreDataContext ctx) : base(ctx) { }

        public IEnumerable<Produto> GetByNome(string nome)
        {
            return _ctx.Produtos.Where(prod => prod.Nome.Contains(nome));
        }

        public IEnumerable<Produto> GetWithTipoProduto()
        {
            return _ctx.Produtos.Include(x => x.TipoProduto).ToList();
        }
    }
}
