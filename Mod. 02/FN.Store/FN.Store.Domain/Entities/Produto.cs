namespace FN.Store.Domain.Entities
{
    public class Produto : Entity
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public int TipoProdutoId { get; set; }
        public TipoProduto TipoProduto { get; set; }
    }
}
