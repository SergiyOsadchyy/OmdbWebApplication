using System.Threading.Tasks;
using ApiCoreModels;

namespace CoreApiClient
{
    public partial class ApiClient
    {
        public async Task<ResponseModel> GetMoviesAsync(string queryString)
        {
            return await GetAsync<ResponseModel>(CreateRequestUri(queryString));
        }
        
        public async Task<MovieExtendedModel> GetMovieAsync(string queryString)
        {
            return await GetAsync<MovieExtendedModel>(CreateRequestUri(queryString));
        }
    }
}