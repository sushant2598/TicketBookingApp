using System.Threading.Tasks;
using TicketService.Models;

namespace TicketService.Service
{
    public interface ITicketBookService
    {
        public Task<int> BookTicket(Ticket ticket, string[] token);
        public Task<bool> CancelTicket(Ticket ticket, string[] token);
    }
}
