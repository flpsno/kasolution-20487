using System.Collections.Generic;

namespace FN.Store.Domain.Entities
{
    public class TipoProduto : Entity
    {
        public string Nome { get; set; }

        public IEnumerable<Produto> Produtos { get; set; }
    }
}
