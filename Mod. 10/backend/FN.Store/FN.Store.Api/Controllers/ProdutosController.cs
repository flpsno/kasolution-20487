using FN.Store.Domain.Contracts.Data;
using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using System;
using System.IO;
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
        private readonly string _uploadFolder;
        private readonly string _storageAccount;

        public ProdutosController(
            IProdutoRepository produtoRepository,
            ITipoProdutoRepository tipoProdutoRepository,
            IConfiguration config,
            IHostingEnvironment env,
            IUnitOfWork uow)
        {
            _produtoRepository = produtoRepository;
            _tipoProdutoRepository = tipoProdutoRepository;
            _uow = uow;
            _uploadFolder = Path.Combine(env.WebRootPath, "uploads");
            _storageAccount = config["StorageAccount"];
        }

        [HttpGet]
        // [AllowAnonymous]
        public async Task<IActionResult> GetProdutos()
        {
            var data = await _produtoRepository.GetAsync();
            return Ok(data);
        }

        [HttpGet("{file}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImage(string file)
        {
            var path = $@"{_uploadFolder}\{file}";
            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            //var conn = CloudStorageAccount.Parse(_storageAccount);
            //var client = conn.CreateCloudBlobClient();
            //var container = client.GetContainerReference("produtos");
            //await container.CreateIfNotExistsAsync();

            //var blob = container.GetBlockBlobReference(file);

            //using (var fs = File.OpenWrite(path))
            //{
            //    await blob.DownloadToStreamAsync(fs);
            //}

            memory.Position = 0;
            return File(memory, "image/jpeg");
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
        public async Task<IActionResult> AddProduto([FromForm] Models.Produtos.PostProduto model,
            IFormFile foto
            //, [FromServices] IHostingEnvironment env
            )
        {
            // Para evitar isso, usar Mediatr (https://github.com/FanSoft-BR/agenda_medica/tree/master/src)


            if (foto == null) ModelState.AddModelError("", "Foto é inválida");

            var tipo = _tipoProdutoRepository.Get(model.TipoProdutoId);
            if (tipo == null)
                ModelState.AddModelError("TipoProdutoId", "Tipo do produto inválido");

            if (ModelState.IsValid)
            {

                var fileName = Guid.NewGuid().ToString("N") + foto.FileName.Substring(foto.FileName.LastIndexOf('.'));
                var filePath = Path.Combine(_uploadFolder, fileName);

                var produto = new Produto()
                {
                    Nome = model.Nome,
                    TipoProdutoId = model.TipoProdutoId,
                    Preco = model.Preco,
                    NomeArquivo = filePath
                };

                _produtoRepository.Add(produto);

                await _uow.CommitAsync();
                // await gravarEmPasta(foto, filePath);
                await gravarNoAzureAsync(foto, fileName, filePath);

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

        private async Task gravarNoAzureAsync(IFormFile foto, string fileName, string filePath)
        {
            var conn = CloudStorageAccount.Parse(_storageAccount);
            var client = conn.CreateCloudBlobClient();
            var container = client.GetContainerReference("produtos");
            await container.CreateIfNotExistsAsync();

            var blob = container.GetBlockBlobReference(fileName);

            // gera o arquivo físico
            await gravarEmPasta(foto, filePath);

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await blob.UploadFromStreamAsync(stream);
            }

        }

        private static async Task gravarEmPasta(IFormFile foto, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await foto.CopyToAsync(fs);
            }
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
