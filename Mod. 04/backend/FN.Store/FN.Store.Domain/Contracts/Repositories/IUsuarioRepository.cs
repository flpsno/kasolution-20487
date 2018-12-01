using FN.Store.Domain.Entities;
using System.Threading.Tasks;

namespace FN.Store.Domain.Contracts.Repositories
{
    public interface IUsuarioRepository: IRepository<Usuario>
    {
        Task<Usuario> GetByEmailAsync(string email);
    }
}
