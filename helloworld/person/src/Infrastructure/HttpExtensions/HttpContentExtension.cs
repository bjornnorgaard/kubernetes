using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infrastructure.HttpExtensions
{
    public static class HttpContentExtension
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            var stringContent = await content.ReadAsStringAsync();
            var deserializeObject = JsonConvert.DeserializeObject<T>(stringContent);

            return deserializeObject;
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient httpClient,
                                                                      string requestUri,
                                                                      object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await httpClient.PostAsync(requestUri, stringContent);
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient httpClient,
                                                                     string requestUri,
                                                                     object model)
        {
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await httpClient.PutAsync(requestUri, stringContent);
        }
    }
}



