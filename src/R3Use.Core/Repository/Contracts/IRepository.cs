using System.Collections.Generic;
using System.Threading.Tasks;
using R3Use.Core.Entities;

namespace R3Use.Core.Repository.Contracts
{
    public interface IRepository<T> where T : new()
    {
        Task<T> GetAsync(int id);

        Task<IList<T>> AllAsync();

        Task SaveAsync(T assignment);
    }
}