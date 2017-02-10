using System.Collections.Generic;
using System.Threading.Tasks;
using NPoco.Core.Repository.Contracts;

namespace NPoco.Core.Repository
{
    public abstract class Repository<T> : IRepository<T>
        where T : new ()
    {
        private readonly IDatabase _database;

        protected Repository(IDatabase database)
        {
            _database = database;
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _database.SingleByIdAsync<T>(id);
        }

        public virtual async Task Add(Prospect prospect)
        {

            await _database.InsertAsync(prospect);
        }

        public virtual async Task<IList<T>> AllAsync()
        {
            return await _database.FetchAsync<T>();
        }
    }
}