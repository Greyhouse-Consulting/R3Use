using System.Collections.Generic;
using System.Threading.Tasks;

namespace NPoco.Core.Repository.Contracts
{
    public interface IRepository<T> where T : new()
    {
        Task<T> GetAsync(int id);

        Task<IList<T>> AllAsync();

    }
}