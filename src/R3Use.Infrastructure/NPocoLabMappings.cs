using NPoco.FluentMappings;
using R3Use.Core.Entities;

namespace R3Use.Infrastructure
{
    public class NPocoLabMappings  : Mappings
    {
        public NPocoLabMappings()
        {
            MappAssignment();

            MappPeriod();
        }

        private void MappPeriod()
        {
            For<Period>().PrimaryKey(k => k.Id, true);

            For<Period>().TableName("periods");

            For<Period>().Columns(x =>
            {
                x.Column(c => c.Description); 
                x.Column(c => c.Start); 
                x.Column(c => c.End); 
                
            });
        }

        private void MappAssignment()
        {
            For<Assignment>().PrimaryKey(k => k.Id, true);

            For<Assignment>().TableName("assignments");

            For<Assignment>().Columns(x =>
            {
                x.Column(c => c.Name);
                x.Column(a => a.Periods).Ignore();
            });
        }
    }
}