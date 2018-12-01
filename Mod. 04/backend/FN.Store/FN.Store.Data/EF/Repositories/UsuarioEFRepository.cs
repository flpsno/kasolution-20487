using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FN.Store.Data.EF.Repositories
{
    public class UsuarioEFRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioEFRepository(StoreDataContext ctx) : base(ctx){}

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _ctx.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
