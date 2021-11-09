using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheatreService.Exceptions;
using TheatreService.Models;
using TheatreService.Service;

namespace TheatreService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheatreController : ControllerBase
    {
        private readonly ITheatreService theatreService;
        public TheatreController(ITheatreService theatreService)
        {
            this.theatreService = theatreService;
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public IActionResult AddTheatre(Theatre theatre)
        {
            try
            {
                return Ok(theatreService.AddTheatre(theatre));
            }
            catch (TheatreAlreadExistsException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public IActionResult DeleteTheatre(int id)
        {
            try
            {
                return Ok(theatreService.DeleteTheatre(id));
            }
            catch (TheatreNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public IActionResult Put(int id, Theatre theatre)
        {
            try
            {
                return Ok(theatreService.UpdateTheatre(id, theatre));
            }
            catch (TheatreNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{imdbId}")]
        public IActionResult GetTheatresByMovieID(string imdbId)
        {
            try
            {
                return Ok(theatreService.GetTheatresByMovieID(imdbId));
            }
            catch (TheatreNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllTheatres()
        {
            try
            {
                return Ok(theatreService.GetAllTheatres());
            }
            catch (TheatreNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("book/{movieId}/{theatreID}/{showDate}/{timings}/{tickets}")]
        //[Authorize(Roles = "CUSTOMER,ADMIN")]
        public IActionResult BookTicket(string movieId, int theatreID, string showDate, string timings, int tickets)
        {
            try
            {
                var foundTheatre = theatreService.GetTheatreById(theatreID);
                if (theatreService.BookTicket(movieId, theatreID, showDate, timings, tickets))
                {
                    return Ok(foundTheatre.Price * tickets);
                }
                else
                {
                    throw new TheatreNotFoundException($"Theatre with id: {theatreID} not found");
                }
            }
            catch (TicketNotGeneratedException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet("cancel/{movieId}/{theatreID}/{showDate}/{timings}/{tickets}")]
        //[Authorize(Roles = "CUSTOMER,ADMIN")]
        public IActionResult CancelTicket(string movieId, int theatreID, string showDate, string timings, int tickets)
        {
            try
            {
                return Ok(theatreService.CancelTicket(movieId, theatreID, showDate, timings, tickets));
            }
            catch (TheatreNotFoundException e)
            {
                return Conflict(e.Message);
            }
        }
    }
}