using System;
using System.Data;
using System.Diagnostics;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository;
using Test.Unit.Setup;
using Xunit;

namespace Test.Unit
{
    [Collection("Test Collection")]
    public class TestBase : IDisposable
    {
        private readonly IDbContextTransaction _transaction;

        public Context Context { get; }
        public IMapper Mapper { get; }

        public TestBase()
        {
            Mapper = TestFixture.Instance.Mapper;

            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = "UnitTests", Mode = SqliteOpenMode.Memory,
                Cache = SqliteCacheMode.Shared
            };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlite(connection);

            Context = new Context(optionsBuilder.Options);
            Context.Database.AutoTransactionsEnabled = false;

            Context.Database.OpenConnection();

            try
            {
                Context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            _transaction = Context.Database.BeginTransaction(IsolationLevel.Serializable);
        }

        public void Dispose()
        {
            _transaction.Rollback();
        }
    }
}



