using System.Data.SqlClient;
using NPoco;
using NPoco.FluentMappings;

namespace R3Use.Core
{
    public class DbTEst
    {
        public void Test()
        {

            var fluentConfig = FluentMappingConfiguration.Configure(new NPocoLabMappings());


            var dbFactory = DatabaseFactory.Config(x =>
            {

                //x.UsingDatabase(() => new Database(new SqlConnection("Server=localhost;Database=npoco;Trusted_Connection=True;")));
                x.UsingDatabase(() => new Database("Server=localhost;Database=npoco;Trusted_Connection=True;", DatabaseType.SqlServer2012, SqlClientFactory.Instance));
                x.WithFluentConfig(fluentConfig);
            });
            
            using (IDatabase db = dbFactory.GetDatabase())
            {
//                db.Connection.Open();
                var users = db.Fetch<Prospect>("select id, name from prospects");

                //              db.Connection.Close();
            }
        }
    }
}