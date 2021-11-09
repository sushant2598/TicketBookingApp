using MovieService.Exceptions;
using MovieService.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieService.Service
{
    public class MovieFetchingService : IMovieFetchingService
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<MovieResponse> FetchMovie(string movieName)
        {
            HttpResponseMessage response = await client.GetAsync($"http://www.omdbapi.com/?apikey=15b720d1&t={movieName}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            MovieResponse movie = JsonConvert.DeserializeObject<MovieResponse>(responseBody);
            /*if (movie == null)
            {
            throw new MovieNotFound("Unable to fetch movie");
            }*/
            return movie;
        }

        public async Task<MovieResponse> FetchMovie(string movieName, int year)
        {
            HttpResponseMessage response = await client.GetAsync($"http://www.omdbapi.com/?apikey=15b720d1&t={movieName}&y=year");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            MovieResponse movie = JsonConvert.DeserializeObject<MovieResponse>(responseBody);
            /*if (movie == null)
            {
            throw new MovieNotFound("Unable to fetch movie");
            }*/
            return movie;
        }
    }
}