using System.Collections.Generic;
using TicketService.Exceptions;
using TicketService.Models;
using TicketService.Repository;

namespace TicketService.Service
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository repository;
        public TicketService(ITicketRepository repository)
        {
            this.repository = repository;
        }
        public bool BookTicket(Ticket ticket)
        {
            var foundTicket = repository.GetTicket(ticket.TicketID);
            if (foundTicket != null)
            {
                throw new TicketCannotBeBookedException("Problem when Booking the Ticket");
            }
            ticket.Status = "BOOKED";
            return repository.BookTicket(ticket);
        }
        public bool CancelTicket(int ticketId)
        {
            var foundTicket = repository.GetTicket(ticketId);
            if (foundTicket == null)
            {
                throw new TicketNotFoundException($"The Ticket with TicketID: {ticketId} not Found");
            }
            return repository.CancelTicket(ticketId);
        }
        public Ticket GetTicket(int ticketId)
        {
            var foundTicket = repository.GetTicket(ticketId);
            if (foundTicket == null)
            {
                throw new TicketNotFoundException($"The Ticket with TicketID: {ticketId} not Found");
            }
            return foundTicket;
        }
        public List<Ticket> GetTicketByUserName(string username)
        {
            var foundTickets = repository.GetTicketByUserName(username);
            if (foundTickets.Count == 0)
            {
                throw new TicketNotFoundException($"No Ticket Details found for {username}");
            }
            return foundTickets;
        }
    }
}
