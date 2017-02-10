using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using NPoco.Core;
using NPoco.Core.Repository;
using NPoco.FluentMappings;
using Xunit;

namespace NPoco.Integration.Tests
{
    public class RepositoryItegrationTests
    {
        private SqliteConnection Connection { get; set; }


        [Fact]
        public async Task Should_Store_And_Load_One_Entity()
        {
            EnsureSharedConnectionConfigured();
            RecreateDataBase();

            var fluentConfig = FluentMappingConfiguration.Configure(new NPocoLabMappings(true));

            var dbFactory = DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() => new Database(Connection));
                x.WithFluentConfig(fluentConfig);
            });

            var db = dbFactory.GetDatabase();

            var repo = new ProspectRepository(db);

            await repo.Add(new Prospect
            {
                Id = 200,
                Name = "Name"
            });

            var i = await repo.GetAsync(200);

            Assert.Equal(200, i.Id);
            Assert.Equal("Name", i.Name);

            CleanupDataBase();
            Dispose();
        }

        public void EnsureSharedConnectionConfigured()
        {
            if (Connection != null)
                return;
            
            //lock (_syncRoot)
            //{
               
                Connection = new SqliteConnection("Data Source=:memory:");
                Connection.Open();
            //}
        }
        public  void RecreateDataBase()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Using SQLite In-Memory DB   ");
            Console.WriteLine("----------------------------");


            var cmd = Connection.CreateCommand();
            cmd.CommandText = "CREATE TABLE prospects(Id INTEGER PRIMARY KEY AUTOINCREMENT, Name nvarchar(200));";
            cmd.ExecuteNonQuery();


            cmd.Dispose();
        }

        public void CleanupDataBase()
        {

            if (Connection == null) return;

            var cmd = Connection.CreateCommand();

            cmd.CommandText = "DROP TABLE prospects;";
            cmd.ExecuteNonQuery();

            cmd.Dispose();
        }

        public virtual void Dispose()
        {
            Console.WriteLine("Disposing connection...     ");

            if (Connection == null) return;

            Connection.Close();
            Connection.Dispose();
        }
    }
}
