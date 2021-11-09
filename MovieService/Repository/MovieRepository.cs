using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MovieService.Context;
using MovieService.Models;

namespace MovieService.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext db;
        public MovieRepository(MovieContext db)
        {
            this.db = db;
        }
        public bool AddMovie(Movies movie)
        {
            var dataFromDB = db.Movies.Find(x => x.ImdbId == movie.ImdbId).FirstOrDefault();
            if (dataFromDB != null)
            {
                return false;
            }
            db.Movies.InsertOne(movie);
            return true;
        }
        public bool DeleteMovie(string imdbId)
        {
            var dataFromDB = db.Movies.Find(x => x.ImdbId == imdbId).FirstOrDefault();
            if (dataFromDB != null)
            {
                db.Movies.DeleteOne(x => x.ImdbId == imdbId);
                return true;
            }
            return false;
        }
        public bool UpdateMovie(string imdbId, Movies movie)
        {
            var dataFromDB = db.Movies.Find(x => x.ImdbId == imdbId).FirstOrDefault();
            if (dataFromDB != null)
            {
                var filter = Builders<Movies>.Filter.Where(x => x.ImdbId == imdbId);
                var update = Builders<Movies>.Update.Set(x => x.MovieName, movie.MovieName)
                .Set(x => x.DateOfRelease, movie.DateOfRelease)
                .Set(x => x.Duration, movie.Duration)
                .Set(x => x.Rating, movie.Rating)
                .Set(x => x.Actors, movie.Actors);
                db.Movies.UpdateOne(filter, update);
                return true;
            }
            return false;
        }
        public Movies GetMovie(string imdbId)
        {
            return db.Movies.Find(x => x.ImdbId == imdbId).FirstOrDefault();
        }
        public List<Movies> GetMovies()
        {
            return db.Movies.Find(x => true).ToList();
        }
    }
}