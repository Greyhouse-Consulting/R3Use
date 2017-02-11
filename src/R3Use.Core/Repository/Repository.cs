using System.Collections.Generic;
using System.Threading.Tasks;
using NPoco;
using R3Use.Core.Entities;
using R3Use.Core.Repository.Contracts;

namespace R3Use.Core.Repository
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

        public virtual async Task Add(Assignment prospect)
        {

            await _database.InsertAsync(prospect);
        }

        public virtual async Task<IList<T>> AllAsync()
        {
            return await _database.FetchAsync<T>();
        }
    }
}