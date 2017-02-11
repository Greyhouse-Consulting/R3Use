using NPoco;
using R3Use.Core.Entities;
using R3Use.Core.Repository.Contracts;

namespace R3Use.Core.Repository
{
    public class ProspectRepository : Repository<Assignment>, IProspectRepository
    {

        public ProspectRepository(IDatabase database) : base(database) { }

    }
}
