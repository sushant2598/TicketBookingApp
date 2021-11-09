using System.Collections.Generic;
using TheatreService.Models;

namespace TheatreService.Service
{
    public interface ITheatreService
    {
        bool AddTheatre(Theatre theatre);
        bool DeleteTheatre(int theatreID);
        bool UpdateTheatre(int theatreID, Theatre theatre);
        Theatre GetTheatreById(int theatreID);
        List<Theatre> GetTheatresByMovieID(string imdbId);
        List<Theatre> GetAllTheatres();
        bool BookTicket(string imdbId, int theatreID, string showDate, string showTimings, int noOfTickets);
        bool CancelTicket(string imdbId, int theatreID, string showDate, string showTimings, int noOfTickets);
    }
}
