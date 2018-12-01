using FN.Store.Domain.Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FN.Store.UI.Controllers
{
    public class ProdutosController: Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }



        public IActionResult Index()
        {
            // var model = _ctx.Produtos.Include(x => x.TipoProduto).ToList();
            var model = _produtoRepository.GetWithTipoProduto();
            return View(model);
        }
         
    }
}
