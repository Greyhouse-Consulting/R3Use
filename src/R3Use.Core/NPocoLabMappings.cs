using NPoco.FluentMappings;

namespace R3Use.Core
{
    public class NPocoLabMappings  : Mappings
    {
        public NPocoLabMappings(bool testMode = false)
        {
            var useAutoincrement = !testMode;

            For<Prospect>().PrimaryKey(k => k.Id, useAutoincrement);

            For<Prospect>().TableName("prospects");

            For<Prospect>().Columns(x =>
            {
                x.Column(c => c.Name);
            });
        }
    }
}