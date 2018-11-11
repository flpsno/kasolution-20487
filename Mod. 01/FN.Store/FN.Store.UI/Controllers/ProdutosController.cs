using FN.Store.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.UI.Controllers
{
    public class ProdutosController: Controller
    {

        public IActionResult Index()
        {
            var produtos = new List<Produto>() {
                new Produto(){ Id = 1, Nome = "Picanha", TipoProduto = "Alimento", Preco=90.5M },
                new Produto(){ Id = 2, Nome = "Pasta de Dente Colgate", TipoProduto = "Higiene", Preco=9.5M },
                new Produto(){ Id = 3, Nome = "Fraldas Pampers", TipoProduto = "Higiene", Preco=80.5M },
            };

            return View(produtos);
        }
         
    }
}
