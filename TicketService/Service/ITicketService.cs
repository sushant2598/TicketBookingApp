using System.Collections.Generic;
using TicketService.Models;

namespace TicketService.Service
{
    public interface ITicketService
    {
        bool BookTicket(Ticket ticket);
        bool CancelTicket(int ticketId);
        Ticket GetTicket(int ticketId);
        List<Ticket> GetTicketByUserName(string username);
    }
}
