using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using NPoco;
using NPoco.FluentMappings;
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
            
            var e = await _database.SingleByIdAsync<T>(id);

            //var sn = _database.StartSnapshot(e);
            
            //var x = FluentMappingConfiguration.Scan(a => a.PrimaryKeysNamed(f => f.AssemblyQualifiedName));

            //var pk = x.Config(_database.Mappers).ForType(typeof(T)).GetPrimaryKeyValues(e);
            //var properties = typeof(T).GetProperties();


            return e;
        }

        public virtual async Task<IList<T>> All()
        {

            return await _database.FetchAsync<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _database.InsertAsync(entity);

        }

        public async  Task DeleteAsync(int id)
        {
            var p = await GetAsync(id);
            await _database.DeleteAsync(p);
        }

        public async Task UpdateAsync(T t)
        {
            await _database.UpdateAsync(t);
        }

        public virtual async Task<IList<T>> AllAsync()
        {
            return await _database.FetchAsync<T>();
        }


        public virtual void Save(T t)
        {
            _database.Save(t);
        }
    }
}