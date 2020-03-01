using System;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;

namespace Test.Integration.Setup
{
    public class TestFixture : IDisposable
    {
        private static TestFixture _instance;
        private static readonly object _padlock = new object();

        public HttpClient Client { get; set; }
        public TestServer Server { get; set; }
        private SqliteConnection Connection { get; }

        public static TestFixture Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (_padlock)
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
                DataSource = "IntegrationTests", Mode = SqliteOpenMode.Memory,
                Cache = SqliteCacheMode.Shared
            };

            Connection = new SqliteConnection(connectionStringBuilder.ToString());

            var factory = new TestWebApplicationFactory<TestStartup>();

            Server = factory.Server;
            Client = factory.CreateClient();
        }

        public void Dispose()
        {
            Connection.Dispose();
            Client.Dispose();
            Server.Dispose();
        }
    }
}



