using System.Collections.Generic;
using TheatreService.Exceptions;
using TheatreService.Models;
using TheatreService.Repository;

namespace TheatreService.Service
{
    public class TheatreService : ITheatreService
    {
        private readonly ITheatreRepository repository;
        public TheatreService(ITheatreRepository repository)
        {
            this.repository = repository;
        }
        public bool AddTheatre(Theatre theatre)
        {
            var foundTheatre = repository.GetTheatreById(theatre.TheatreID);
            if (foundTheatre != null)
            {
                throw new TheatreAlreadExistsException($"Theatre with ID: {theatre.TheatreID} already exists");
            }
            return repository.AddTheatre(theatre);
        }
        public bool DeleteTheatre(int theatreID)
        {
            var foundTheatre = repository.GetTheatreById(theatreID);
            if (foundTheatre == null)
            {
                throw new TheatreNotFoundException($"Theatre with ID: {theatreID} not Found");
            }
            return repository.DeleteTheatre(theatreID);
        }
        public bool UpdateTheatre(int theatreID, Theatre theatre)
        {
            var newTheatre = repository.GetTheatreById(theatreID);
            if (newTheatre == null)
            {
                throw new TheatreNotFoundException($"Theatre with ID: {theatreID} not Found");
            }
            return repository.UpdateTheatre(theatreID, theatre);
        }
        public Theatre GetTheatreById(int theatreID)
        {
            var foundTheatre = repository.GetTheatreById(theatreID);
            if (foundTheatre == null)
            {
                throw new TheatreNotFoundException($"Theatre with ID: {theatreID} not Found");
            }
            return foundTheatre;
        }
        public List<Theatre> GetTheatresByMovieID(string movieId)
        {
            var foundTheatres = repository.GetTheatresByMovieID(movieId);
            if (foundTheatres.Count == 0)
            {
                throw new TheatreNotFoundException($"No Theatre are present for movie id:{movieId}");
            }
            return foundTheatres;
        }
        public List<Theatre> GetAllTheatres()
        {
            var foundTheatres = repository.GetAllTheatres();
            if (foundTheatres.Count == 0)
            {
                throw new TheatreNotFoundException("No Theatre Details are present");
            }
            return foundTheatres;
        }
        public bool BookTicket(string imdbId, int theatreID, string showDate, string showTimings, int noOfTickets)
        {
            var foundTheatre = repository.GetTheatreById(theatreID);
            if (foundTheatre == null)
            {
                throw new TheatreNotFoundException($"Theatre with ID: {theatreID} not Found");
            }
            return repository.BookTicket(imdbId, theatreID, showDate, showTimings, noOfTickets);
        }
        public bool CancelTicket(string imdbId, int theatreID, string showDate, string showTimings, int noOfTickets)
        {
            var foundTheatre = repository.GetTheatreById(theatreID);
            if (foundTheatre == null)
            {
                throw new TheatreNotFoundException($"Theatre with ID: {theatreID} not Found");
            }
            return repository.CancelTicket(imdbId, theatreID, showDate, showTimings, noOfTickets);
        }
    }
}