using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OmdbWebApplication.Factory;
using OmdbWebApplication.Models;
using OmdbWebApplication.Utility;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OmdbWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _apikey;
        private readonly IMovieProvider _movieProvider;

        public HomeController(IConfiguration configuration, IMovieProvider movieProvider)
        {
            ApplicationSettings.WebApiUrl = configuration.GetValue<string>("MySettings:WebApiBaseUrl");
            _apikey = configuration.GetValue<string>("MySettings:ApiKey");
            _movieProvider = movieProvider;
        }
       
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Results(Search search)
        {
            string searchPhrase = String.Join("+", search.MovieTitle.Split(' '));
            var queryString = $"?s={searchPhrase}&plot=full&apikey={_apikey}";
            var data = await ApiClientFactory.Instance.GetMoviesAsync(queryString);
            // TODO: develop pagination
            return View(data);
        }
        
        public async Task<IActionResult> Movie(string id)
        {
            var data = await _movieProvider.GetAsync(id);

            if (data == null)
            {
                var queryString = $"?i={id}&plot=full&apikey={_apikey}";
                data = await ApiClientFactory.Instance.GetMovieAsync(queryString);
                await _movieProvider.CreateAsync(data);
            }

            return View(data);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}