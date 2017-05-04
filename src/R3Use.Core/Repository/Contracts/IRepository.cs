using System.Collections.Generic;
using System.Threading.Tasks;

namespace R3Use.Core.Repository.Contracts
{
    public interface IRepository<T> where T : new()
    {
        Task<T> GetAsync(int id);

        Task<IList<T>> AllAsync();

        Task AddAsync(T assignment);

        Task DeleteAsync(int id);


        Task UpdateAsync(T t);
    }
}