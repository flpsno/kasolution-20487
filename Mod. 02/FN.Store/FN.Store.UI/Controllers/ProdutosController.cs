using FN.Store.Data.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FN.Store.UI.Controllers
{
    public class ProdutosController: Controller
    {
        private readonly StoreDataContext _ctx;
        //public ProdutosController(StoreDataContext ctx)
        //{
        //    _ctx = ctx;
        //}
        public ProdutosController(StoreDataContext ctx) => _ctx = ctx;



        public IActionResult Index()
        {
            var model = _ctx.Produtos.Include(x => x.TipoProduto).ToList();
            return View(model);
        }
         
    }
}
