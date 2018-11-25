using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FN.Store.Api.Controllers
{
    public class TesteController
    {

        [Route("teste")]
        public string Ping()
        {
            return "Pong";
        }

    }
}
