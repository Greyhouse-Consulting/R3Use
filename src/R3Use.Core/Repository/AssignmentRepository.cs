using NPoco;
using R3Use.Core.Entities;
using R3Use.Core.Repository.Contracts;

namespace R3Use.Core.Repository
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {

        public AssignmentRepository(IDatabase database) : base(database) { }

    }
}
