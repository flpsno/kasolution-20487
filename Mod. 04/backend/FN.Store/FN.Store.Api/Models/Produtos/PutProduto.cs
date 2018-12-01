using System.ComponentModel.DataAnnotations;

namespace FN.Store.Api.Models.Produtos
{
    public class PutProduto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Valor inválido")]
        public decimal Preco { get; set; }
    }
}
