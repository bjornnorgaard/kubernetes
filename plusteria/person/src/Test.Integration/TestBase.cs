using System;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using MediatR;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Test.Integration.Setup;
using Xunit;

namespace Test.Integration
{
    [Collection("Test Collection")]
    public class TestBase : IDisposable
    {
        private readonly IDbContextTransaction _transaction;

        private IServiceProvider ServiceProvider => Server?.Host?.Services;

        public Context Context { get; set; }
        public HttpClient Client { get; set; }
        public TestServer Server { get; set; }

        public IMediator Mediator { get; set; }

        protected TestBase()
        {
            Client = TestFixture.Instance.Client;
            Server = TestFixture.Instance.Server;

            var serviceScope =
                ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            Context = serviceScope.ServiceProvider.GetService<Context>();
            Mediator = serviceScope.ServiceProvider.GetService<IMediator>();

            Context.Database.OpenConnection();

            try
            {
                Context.Database.EnsureCreated();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            _transaction = Context.Database.BeginTransaction(IsolationLevel.Serializable);
        }

        public void Dispose()
        {
            _transaction.Rollback();
        }
    }
}



