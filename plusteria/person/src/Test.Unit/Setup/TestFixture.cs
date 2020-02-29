using System;
using Api.Web;
using Application;
using AutoMapper;
using Microsoft.Data.Sqlite;

namespace Test.Unit.Setup
{
    public class TestFixture : IDisposable
    {
        private static TestFixture _instance;
        private static readonly object padlock = new object();

        private SqliteConnection Connection { get; }

        public IMapper Mapper { get; }

        public static TestFixture Instance
        {
            get
            {
                if (_instance == null)
                    lock (padlock)
                    {
                        if (_instance == null) _instance = new TestFixture();
                    }

                return _instance;
            }
        }

        public TestFixture()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "UnitTests", Mode = SqliteOpenMode.Memory,
                Cache = SqliteCacheMode.Shared
            };

            Connection = new SqliteConnection(connectionStringBuilder.ToString());

            var config = new MapperConfiguration(
                                                 cfg =>
                                                 {
                                                     cfg.AddMaps(typeof(AssemblyAnchor).Assembly,
                                                                 typeof(Startup).Assembly);
                                                 });

            Mapper = new Mapper(config);
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}



