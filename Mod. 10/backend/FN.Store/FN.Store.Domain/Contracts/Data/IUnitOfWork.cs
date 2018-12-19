using System.Threading.Tasks;

namespace FN.Store.Domain.Contracts.Data
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task RollBackAsync();
    }
}
