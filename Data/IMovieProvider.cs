using ApiCoreModels;
using System.Threading.Tasks;

namespace Data
{
    public interface IMovieProvider
    {
        Task<MovieExtendedModel> GetAsync(string id);
        Task CreateAsync(MovieExtendedModel movie);
    }
}