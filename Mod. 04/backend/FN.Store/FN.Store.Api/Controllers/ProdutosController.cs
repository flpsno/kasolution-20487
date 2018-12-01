using FN.Store.Domain.Contracts.Data;
using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FN.Store.Api.Controllers
{
    // [Authorize(Roles ="Admin", Policy = "addUser")]
    [Authorize(Roles = "Admin")]
    [Route("api/v1/[controller]")]
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ITipoProdutoRepository _tipoProdutoRepository;
        private readonly IUnitOfWork _uow;

        public ProdutosController(
            IProdutoRepository produtoRepository,
            ITipoProdutoRepository tipoProdutoRepository,
            IUnitOfWork uow)
        {
            _produtoRepository = produtoRepository;
            _tipoProdutoRepository = tipoProdutoRepository;
            _uow = uow;
        }

        [HttpGet]
        // [AllowAnonymous]
        public async Task<IActionResult> GetProdutos()
        {
            var data = await _produtoRepository.GetAsync();
            return Ok(data);
        }

        //[Route("{id:int}")]
        [HttpGet("{id:int}", Name = "GetProdutoById")]
        public async Task<IActionResult> GetProdutoById(int id)
        {
            var data = await _produtoRepository.GetAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduto([FromBody] Models.Produtos.PostProduto model
            // , [FromServices]IUnitOfWork uow => a DI coloca o objeto para vc
            )
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

                await _uow.CommitAsync();

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
        public async Task<IActionResult> EditProduto(int id, [FromBody] Models.Produtos.PutProduto model)
        {
            var produto = _produtoRepository.Get(id);
            if (produto == null)
                ModelState.AddModelError("Id", "Produto não localizado");

            if (ModelState.IsValid)
            {
                produto.Alterar(model.Nome, model.Preco);
                _produtoRepository.Edit(produto);

                await _uow.CommitAsync();
                return NoContent();
                // return Ok(produto);
            }

            return BadRequest(ModelState);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DelProduto(int id)
        {
            var produto = _produtoRepository.Get(id);
            if (produto == null)
                return BadRequest(new { Id = new string[] { "Produto não localizado" } });

            _produtoRepository.Delete(produto);

            await _uow.CommitAsync();
            return NoContent();
        }



    }
}
