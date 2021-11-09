using System.Collections.Generic;
using MovieService.Models;

namespace MovieService.Repository
{
    public interface IMovieRepository
    {
        bool AddMovie(Movies movie);
        bool DeleteMovie(string imdbId);
        bool UpdateMovie(string imdbId, Movies movie);
        Movies GetMovie(string imdbId);
        List<Movies> GetMovies();
    }
}
