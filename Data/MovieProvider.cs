using ApiCoreModels;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data
{
    public class MovieProvider : IMovieProvider
    {
        private readonly string _connectionString;
        
        public MovieProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<MovieExtendedModel> GetAsync(string id)
        {
            MovieExtendedModel movie;
            IEnumerable<RatingModel> ratings;
            string sqlMovies = "SELECT Title, Year, Rated, Released, Runtime, Genre, Director, Writer, Actors, Plot, " +
                         "Language, Country, Awards, Poster, Metascore, ImdbRating, ImdbVotes, ImdbId, Type, DVD, " +
                         $"BoxOffice, Production, Website FROM Movies WHERE ImdbId = @ImdbId";
            string sqlRatings = $"SELECT Source, Value FROM Ratings WHERE ImdbId = @ImdbId";
            
            using (var connection = new SqlConnection(_connectionString))
            {
                movie = await connection.QueryFirstOrDefaultAsync<MovieExtendedModel>(sqlMovies, new {ImdbId = id});

                if (movie != null)
                {
                    ratings = await connection.QueryAsync<RatingModel>(sqlRatings, new {ImdbId = id});

                    if (ratings != null)
                    {
                        foreach (var rating in ratings)
                        {
                            movie.Ratings.Add(rating);
                        }
                    }
                }
            }
            return movie;
        }

        public async Task CreateAsync(MovieExtendedModel movie)
        {
            string sqlMovie = $"INSERT INTO Movies " +
                         $"(Title, Year, Rated, Released, Runtime, Genre, Director, " +
                         $"Writer, Actors, Plot, Language, Country, Awards, Poster, " +
                         $"Metascore, ImdbRating, ImdbVotes, ImdbId, Type, DVD, BoxOffice, Production, Website) " +
                         $"VALUES " +
                         $"(@Title, @Year, @Rated, @Released, @Runtime, @Genre, @Director, " +
                         $"@Writer, @Actors, @Plot, @Language, @Country, @Awards, @Poster, " +
                         $"@Metascore, @ImdbRating, @ImdbVotes, @ImdbId, @Type, @DVD, @BoxOffice, @Production, @Website)";
            string sqlRatings = $"INSERT INTO Ratings (ImdbId, Source, Value) VALUES (@ImdbId, @Source, @Value)";
            
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlMovie, new
                {
                    movie.Title, movie.Year, movie.Rated, movie.Released, movie.Runtime, movie.Genre,
                    movie.Director, movie.Writer, movie.Actors, movie.Plot, movie.Language, movie.Country,
                    movie.Awards, movie.Poster, movie.Metascore, movie.ImdbRating, movie.ImdbVotes, movie.ImdbId,
                    movie.Type, movie.DVD, movie.BoxOffice, movie.Production, movie.Website
                });

                if (movie.Ratings.Count > 0)
                {
                    foreach (var rating in movie.Ratings)
                    {
                        await connection.ExecuteAsync(sqlRatings, new {movie.ImdbId, rating.Source, rating.Value});
                    }
                }
            }
        }
    }
}