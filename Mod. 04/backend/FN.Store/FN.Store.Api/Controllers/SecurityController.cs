using FN.Store.Api.Models;
using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using FN.Store.Domain.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class SecurityController: Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _config;

        public SecurityController(
            IUsuarioRepository usuarioRepository,
            IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> RequestToken([FromBody]LoginVM login)
        {

            if (!string.IsNullOrWhiteSpace(login.Email) && !string.IsNullOrWhiteSpace(login.Senha))
            {
                var user = await _usuarioRepository.GetByEmailAsync(login.Email);

                if (user != null && user.Senha == login.Senha.Encrypt())
                {
                    return await GenerateToken(user);
                }
            }

            return BadRequest("Dados inválidos");
        }

        private async Task<OkObjectResult> GenerateToken(Usuario user)
        {
            var claims = new[] {

                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Nome),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "TI"),
                //new Claim("permissions","addUser")
            };

            var key = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "fansoft",
                audience: "fansoft/client",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials
            );

            return Ok(
                new { token = new JwtSecurityTokenHandler().WriteToken(token) }
                );
        }
    }
}
