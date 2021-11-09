using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieService.Exceptions;
using MovieService.Models;
using MovieService.Service;

namespace MovieService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService movieService;
        private readonly IMovieFetchingService movieFetchingService;
        public MoviesController(IMovieService movieService, IMovieFetchingService movieFetchingService)
        {
            this.movieService = movieService;
            this.movieFetchingService = movieFetchingService;
        }

        [HttpPost("{movieName}/{year}")]
        //[Authorize(Roles = "ADMIN")]
        public IActionResult AddMovie(string movieName, int year)
        {
            try
            {
                MovieResponse movie = movieFetchingService.FetchMovie(movieName, year).Result;
                if (movie != null)
                {
                    Movies newMovie = new Movies() { MovieName = movie.Title, ImdbId = movie.imdbID, DateOfRelease = movie.Released, Duration = movie.Runtime, Rating = movie.imdbRating, Actors = movie.Actors, ImagePath = movie.Poster };
                    movieService.AddMovie(newMovie);
                    return Ok("Movie Added");
                }
                else
                {
                    throw new MovieNotAddedException("Error While adding Movie");
                }
            }
            catch (MovieIDAlreadyExists e)
            {
                return Conflict(e.Message);
            }
            catch (MovieNotAddedException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpDelete("{imdbId}")]
        //[Authorize(Roles = "ADMIN")]
        public IActionResult DeleteMovie(string imdbId)
        {
            try
            {
                return Ok(movieService.DeleteMovie(imdbId));
            }
            catch (MovieNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{imdbId}")]
        //[Authorize(Roles = "ADMIN")]
        public IActionResult UpdateMovie(string imdbId, Movies movie)
        {
            try
            {
                return Ok(movieService.UpdateMovie(imdbId, movie));
            }
            catch (MovieNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{imdbId}")]
        public IActionResult GetMovieByID(string imdbId)
        {
            try
            {
                return Ok(movieService.GetMovie(imdbId));
            }
            catch (MovieNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            try
            {
                return Ok(movieService.GetMovies());
            }
            catch (MovieNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{movieName}/{year}")]
        //[Authorize(Roles = "ADMIN")]
        public IActionResult GetMovieByExternalAPI(string movieName, int year)
        {
            try
            {
                MovieResponse movie = movieFetchingService.FetchMovie(movieName, year).Result;
                if (movie != null)
                {
                    return Ok(movie);
                }
                else
                {
                    throw new MovieNotFoundException("Movie Details Not Found");
                }
            }
            catch (MovieIDAlreadyExists e)
            {
                return Conflict(e.Message);
            }
            catch (MovieNotAddedException e)
            {
                return Conflict(e.Message);
            }
        }
    }
}
