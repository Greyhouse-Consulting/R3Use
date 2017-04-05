using System;
using Microsoft.Data.Sqlite;
using NPoco;
using NPoco.FluentMappings;

namespace R3Use.Infrastructure
{
    public  class SqliteIntegration
    {


        protected SqliteConnection Connection { get; set; }



        public Database CreateDatabase()
        {
            var fluentConfig = FluentMappingConfiguration.Configure(new NPocoLabMappings(true));

            var dbFactory = DatabaseFactory.Config(x =>
            {
                x.UsingDatabase(() => new Database(Connection, DatabaseType.SQLite));
                x.WithFluentConfig(fluentConfig);
            });


            return dbFactory.GetDatabase();
        }
        protected void Setup()
        {
            EnsureSharedConnectionConfigured();
            RecreateDataBase();
        }

        protected void TearDown()
        {
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
        public void RecreateDataBase()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Using SQLite In-Memory DB   ");
            Console.WriteLine("----------------------------");


            var cmd = Connection.CreateCommand();
            cmd.CommandText = "CREATE TABLE assignments(Id INTEGER PRIMARY KEY, Name nvarchar(200));";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "CREATE TABLE periods(Id INTEGER PRIMARY KEY, Description nvarchar(200), Start TEXT, End TEXT);";
            cmd.ExecuteNonQuery();

            cmd.Dispose();
        }

        public void CleanupDataBase()
        {

            if (Connection == null) return;

            var cmd = Connection.CreateCommand();

            cmd.CommandText = "DROP TABLE assignments;";
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
