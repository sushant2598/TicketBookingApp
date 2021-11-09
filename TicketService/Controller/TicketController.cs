using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using TicketService.Exceptions;
using TicketService.Models;
using TicketService.Service;

namespace TicketService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService ticketService;
        private readonly ITicketBookService ticketBookService;
        private readonly IFetchedTokenParser fetchedTokenParser;
        public TicketController(ITicketService ticketService, ITicketBookService ticketBookService, IFetchedTokenParser fetchedTokenParser)
        {
            this.ticketService = ticketService;
            this.ticketBookService = ticketBookService;
            this.fetchedTokenParser = fetchedTokenParser;
        }

        [HttpPost("book")]
        public IActionResult BookTicket(Ticket ticket)
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                Request.Headers.TryGetValue("Authorization", out authorizationToken);
                var tokenValues = fetchedTokenParser.tokenValues(authorizationToken);
                var totalcost = ticketBookService.BookTicket(ticket, tokenValues).Result;

                if (totalcost > 0)
                {
                    ticket.TotalCost = totalcost;
                    return Ok(ticketService.BookTicket(ticket));
                }
                else
                {
                    throw new TicketCannotBeBookedException("Error while booking the tickets");
                }
            }
            catch (TicketCannotBeBookedException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpPost("cancel/{ticketId}")]
        public IActionResult CancelTicket(int ticketId)
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues authorizationToken;
                Request.Headers.TryGetValue("Authorization", out authorizationToken);
                var tokenValues = fetchedTokenParser.tokenValues(authorizationToken);

                var foundTicket = ticketService.GetTicket(ticketId);
                if (ticketBookService.CancelTicket(foundTicket, tokenValues).Result)
                {
                    return Ok(ticketService.CancelTicket(ticketId));
                }
                else
                {
                    throw new TicketCannotBeCancelledException($"Details of TicketId: {ticketId} cannot be found");
                }
            }
            catch (TicketNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (TicketCannotBeCancelledException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet("{ticketId:int}")]
        public IActionResult GetTicket(int ticketId)
        {
            try
            {
                return Ok(ticketService.GetTicket(ticketId));
            }
            catch (TicketNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{username}")]
        public IActionResult GetTicketByUsername(string username)
        {
            try
            {
                return Ok(ticketService.GetTicketByUserName(username));
            }
            catch (TicketNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}