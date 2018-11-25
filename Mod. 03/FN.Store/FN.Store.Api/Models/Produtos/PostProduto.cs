using System.ComponentModel.DataAnnotations;

namespace FN.Store.Api.Models.Produtos
{
    public class PostProduto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }

        public int TipoProdutoId { get; set; }

        public decimal Preco { get; set; }
    }
}
