using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreApiClient
{
    public partial class ApiClient
    {
        private Uri _baseEndpoint;
        private readonly HttpClient _httpClient;

        public ApiClient(Uri baseEndPoint)
        {
            if (baseEndPoint == null)
            {
                throw new ArgumentNullException("baseEndPoint");
            }

            _baseEndpoint = baseEndPoint;
            _httpClient = new HttpClient();
        }

        private async Task<T> GetAsync<T>(Uri requestUrl)
        {
            var responce = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseContentRead);
            responce.EnsureSuccessStatusCode();
            var data = await responce.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        private Uri CreateRequestUri(string queryString)
        {
            var uriBuilder = new UriBuilder(_baseEndpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }
    }
}