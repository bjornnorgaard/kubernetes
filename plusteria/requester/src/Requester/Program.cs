using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Requester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var getUrl = Environment.GetEnvironmentVariable("REQUEST_URL_ENDPOINT");
            if (string.IsNullOrWhiteSpace(getUrl)) getUrl = "http://localhost:5000/api/v1/Person?take=10";
            
            var sleepTimeEnv = Environment.GetEnvironmentVariable("SLEEP_TIME");
            var parseSuccess = int.TryParse(sleepTimeEnv, out var sleepTime);
            if (parseSuccess == false) sleepTime = 100;

            var client = new HttpClient();
            
            while (true)
            {
                Console.WriteLine($"Starting request to '{getUrl}'");
                try
                {
                    await client.GetAsync(getUrl);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"I caught an exception: {e}");
                    Thread.Sleep(5000);
                }
                Thread.Sleep(sleepTime);
            }
        }
    }
}
