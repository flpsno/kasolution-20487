using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ITipoProdutoRepository _tipoProdutoRepository;

        public ProdutosController(IProdutoRepository produtoRepository,
            ITipoProdutoRepository tipoProdutoRepository)
        {
            _produtoRepository = produtoRepository;
            _tipoProdutoRepository = tipoProdutoRepository;
        }

        [HttpGet]
        public IActionResult GetProdutos()
        {
            var data = _produtoRepository.Get();
            return Ok(data);
        }

        //[Route("{id:int}")]
        [HttpGet("{id:int}", Name = "GetProdutoById")]
        public IActionResult GetProdutoById(int id)
        {
            var data = _produtoRepository.Get(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }


        [HttpPost]
        public IActionResult AddProduto([FromBody] Models.Produtos.PostProduto model)
        {
            // Para evitar isso, usar Mediatr (https://github.com/FanSoft-BR/agenda_medica/tree/master/src)

            var tipo = _tipoProdutoRepository.Get(model.TipoProdutoId);
            if (tipo == null)
                ModelState.AddModelError("TipoProdutoId", "Tipo do produto inválido");

            if (ModelState.IsValid)
            {
                var produto = new Produto()
                {
                    Nome = model.Nome,
                    TipoProdutoId = model.TipoProdutoId,
                    Preco = model.Preco
                };

                _produtoRepository.Add(produto);

                return CreatedAtRoute("GetProdutoById", new { produto.Id },
                        new
                        {
                            produto.Id,
                            produto.Nome,
                            produto.Preco,
                            Tipo = tipo.Nome
                        }
                    );
            }


            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public IActionResult EditProduto(int id, [FromBody] Models.Produtos.PutProduto model)
        {
            var produto = _produtoRepository.Get(id);
            if (produto == null)
                ModelState.AddModelError("Id", "Produto não localizado");

            if (ModelState.IsValid)
            {
                produto.Alterar(model.Nome, model.Preco);
                _produtoRepository.Edit(produto);
                return NoContent();
                // return Ok(produto);
            }

            return BadRequest(ModelState);

        }

        [HttpDelete("{id:int}")]
        public IActionResult DelProduto(int id)
        {
            var produto = _produtoRepository.Get(id);
            if (produto == null)
                return BadRequest(new { Id = new string[] { "Produto não localizado" } });

            _produtoRepository.Delete(produto);

            return NoContent();
        }



    }
}
