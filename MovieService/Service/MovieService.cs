using System.Collections.Generic;
using MovieService.Exceptions;
using MovieService.Models;
using MovieService.Repository;

namespace MovieService.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository repository;
        public MovieService(IMovieRepository repository)
        {
            this.repository = repository;
        }
        public bool AddMovie(Movies movie)
        {
            var data = repository.GetMovie(movie.ImdbId);
            if (data != null)
            {
                throw new MovieIDAlreadyExists($"Movie with {movie.ImdbId} already exists");
            }
            if (repository.AddMovie(movie))
            {
                return true;
            }
            else
            {
                throw new MovieNotAddedException("Movie is not added.");
            }
        }
        public bool DeleteMovie(string imdbId)
        {
            var data = repository.GetMovie(imdbId);
            if (data == null)
            {
                throw new MovieNotFoundException($"Movie with {imdbId} not found");
            }
            return repository.DeleteMovie(imdbId);
        }
        public bool UpdateMovie(string imdbId, Movies movie)
        {
            var data = repository.GetMovie(imdbId);
            if (data == null)
            {
                throw new MovieNotFoundException($"Movie with {imdbId} not found");
            }
            return repository.UpdateMovie(imdbId, movie);
        }
        public Movies GetMovie(string imdbId)
        {
            var data = repository.GetMovie(imdbId);
            if (data == null)
            {
                throw new MovieNotFoundException($"Movie with {imdbId} not found");
            }
            return data;
        }
        public List<Movies> GetMovies()
        {
            List<Movies> movies = repository.GetMovies();
            if (movies.Count == 0)
            {
                throw new MovieNotFoundException("No Movie is present.");
            }
            return movies;
        }
    }
}