using System.Data.SqlClient;
using NPoco;
using NPoco.FluentMappings;

namespace R3Use.Core
{
    public class DbProvider : IDbProvider
    {
        public IDatabase Create()
        {
            var fluentConfig = FluentMappingConfiguration.Configure(new NPocoLabMappings());

            var dbFactory = DatabaseFactory.Config(x =>
            {

                //x.UsingDatabase(() => new Database(new SqlConnection("Server=localhost;Database=npoco;Trusted_Connection=True;")));
                x.UsingDatabase(() => new Database("Server=localhost;Database=npoco;Trusted_Connection=True;", DatabaseType.SqlServer2012, SqlClientFactory.Instance));
                x.WithFluentConfig(fluentConfig);
            });


            return dbFactory.GetDatabase();
        }
    }
}