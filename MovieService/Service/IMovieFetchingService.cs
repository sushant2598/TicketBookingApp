using MovieService.Models;
using System.Threading.Tasks;

namespace MovieService.Service
{
    public interface IMovieFetchingService
    {
        public Task<MovieResponse> FetchMovie(string movieName);
        public Task<MovieResponse> FetchMovie(string movieName, int year);
    }
}
