using NPoco.Core.Repository.Contracts;

namespace NPoco.Core.Repository
{
    public class ProspectRepository : Repository<Prospect>, IProspectRepository
    {

        public ProspectRepository(IDatabase database) : base(database) { }

    }
}
