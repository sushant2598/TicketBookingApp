using System.Collections.Generic;
using TicketService.Models;

namespace TicketService.Repository
{
    public interface ITicketRepository
    {
        bool BookTicket(Ticket ticket);
        bool CancelTicket(int ticketId);
        Ticket GetTicket(int ticketId);
        List<Ticket> GetTicketByUserName(string username);
    }
}
