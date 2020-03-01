using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Test.Integration.Setup
{
    public class TestWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder(null).UseStartup<TEntryPoint>();
        }
    }
}



