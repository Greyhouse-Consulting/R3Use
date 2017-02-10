using NPoco;
using R3Use.Core.Repository.Contracts;

namespace R3Use.Core.Repository
{
    public class ProspectRepository : Repository<Prospect>, IProspectRepository
    {

        public ProspectRepository(IDatabase database) : base(database) { }

    }
}
