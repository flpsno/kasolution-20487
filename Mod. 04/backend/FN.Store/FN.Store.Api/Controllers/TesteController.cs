using Microsoft.AspNetCore.Mvc;

namespace FN.Store.Api.Controllers
{
    public class TesteController
    {

        [HttpGet("teste")]
        public string Ping()
        {
            return "Pong";
        }

    }
}
