using NPoco.FluentMappings;
using R3Use.Core.Entities;

namespace R3Use.Infrastructure
{
    public class NPocoLabMappings  : Mappings
    {
        public NPocoLabMappings(bool testMode = false)
        {
            var useAutoincrement = !testMode;

            For<Assignment>().PrimaryKey(k => k.Id, useAutoincrement);

            For<Assignment>().TableName("prospects");

            For<Assignment>().Columns(x =>
            {
                x.Column(c => c.Name);
            });
        }
    }
}