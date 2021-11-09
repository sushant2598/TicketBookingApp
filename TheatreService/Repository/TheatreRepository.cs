using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using TheatreService.Context;
using TheatreService.Exceptions;
using TheatreService.Models;

namespace TheatreService.Repository
{
    public class TheatreRepository : ITheatreRepository
    {
        private readonly TheatreContext db;
        public TheatreRepository(TheatreContext db)
        {
            this.db = db;
        }
        public bool AddTheatre(Theatre theatre)
        {
            var dataFromDB = db.Theatres.Find(x => x.TheatreID == theatre.TheatreID).FirstOrDefault();
            if (dataFromDB != null)
            {
                return false;
            }
            db.Theatres.InsertOne(theatre);
            return true;
        }
        public bool DeleteTheatre(int theatreID)
        {
            var dataFromDB = db.Theatres.Find(x => x.TheatreID == theatreID).FirstOrDefault();
            if (dataFromDB == null)
            {
                return false;
            }
            db.Theatres.DeleteOne(x => x.TheatreID == theatreID);
            return true;
        }
        public bool UpdateTheatre(int theatreID, Theatre theatre)
        {
            var dataFromDB = db.Theatres.Find(x => x.TheatreID == theatreID).FirstOrDefault();
            if (dataFromDB == null)
            {
                return false;
            }
            var filter = Builders<Theatre>.Filter.Where(x => x.TheatreID == theatreID);
            var update = Builders<Theatre>.Update
            .Set(x => x.TheatreName, theatre.TheatreName)
            .Set(x => x.TheatreAddress, theatre.TheatreAddress)
            .Set(x => x.Shows, theatre.Shows)
            .Set(x => x.Price, theatre.Price);
            db.Theatres.UpdateOne(filter, update);
            return true;
        }
        public Theatre GetTheatreById(int theatreID)
        {
            return db.Theatres.Find(x => x.TheatreID == theatreID).FirstOrDefault();
        }
        public List<Theatre> GetTheatresByMovieID(string movieId)
        {
            return db.Theatres.Find(x => x.Shows.Any(a => a.ShowsDetails.Any(b => b.MovieImdbID == movieId))).ToList();
        }
        public List<Theatre> GetAllTheatres()
        {
            return db.Theatres.Find(x => true).ToList();
        }
        public bool BookTicket(string imdbId, int theatreID, string showDate, string showTimings, int noOfTickets)
        {
            Theatre selectedTheatre = db.Theatres.Find(x => x.TheatreID == theatreID).FirstOrDefault();

            Show selectedShow = selectedTheatre.Shows.Where(x => x.ShowDate == showDate).FirstOrDefault();

            var checkMoviePresence = selectedShow.ShowsDetails.Where(x => x.MovieImdbID == imdbId).FirstOrDefault();

            if (selectedTheatre == null && selectedShow == null && checkMoviePresence == null)
            {
                return false;
            }

            int index = selectedTheatre.Shows.IndexOf(selectedShow);

            foreach (var item in selectedShow.ShowsDetails.Where(x => x.ShowTimings == showTimings && x.MovieImdbID == imdbId))
            {
                int newAvailableSeats = item.AvailableSeats - noOfTickets;
                if (newAvailableSeats < 0)
                {
                    throw new TicketNotGeneratedException($"Only {item.AvailableSeats} ticekts are available for this show");
                }
                item.AvailableSeats = newAvailableSeats;
            }

            selectedTheatre.Shows[index] = selectedShow;
            var filter = Builders<Theatre>.Filter.Where(x => x.TheatreID == theatreID);
            var update = Builders<Theatre>.Update
            .Set(x => x.Shows, selectedTheatre.Shows);
            db.Theatres.UpdateOne(filter, update);
            return true;
        }
        public bool CancelTicket(string imdbId, int theatreID, string showDate, string showTimings, int noOfTickets)
        {
            Theatre selectedTheatre = db.Theatres.Find(x => x.TheatreID == theatreID).FirstOrDefault();

            Show selectedShow = selectedTheatre.Shows.Where(x => x.ShowDate == showDate).FirstOrDefault();

            var checkMoviePresence = selectedShow.ShowsDetails.Where(x => x.MovieImdbID == imdbId).FirstOrDefault();

            if (selectedTheatre == null && selectedShow == null && checkMoviePresence == null)
            {
                return false;
            }

            int index = selectedTheatre.Shows.IndexOf(selectedShow);

            foreach (var item in selectedShow.ShowsDetails.Where(x => x.ShowTimings == showTimings && x.MovieImdbID == imdbId))
            {
                int newAvailableSeats = item.AvailableSeats + noOfTickets;
                item.AvailableSeats = newAvailableSeats;
            }

            selectedTheatre.Shows[index] = selectedShow;
            var filter = Builders<Theatre>.Filter.Where(x => x.TheatreID == theatreID);
            var update = Builders<Theatre>.Update
            .Set(x => x.Shows, selectedTheatre.Shows);
            db.Theatres.UpdateOne(filter, update);
            return true;
        }
    }
}
