using System.Collections.Generic;
using System.Linq;
using TicketService.Context;
using TicketService.Models;

namespace TicketService.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketContext db;
        public TicketRepository(TicketContext db)
        {
            this.db = db;
        }
        public bool BookTicket(Ticket ticket)
        {
            var foundTicket = db.Tickets.Where(x => x.TicketID == ticket.TicketID).FirstOrDefault();
            if (foundTicket != null)
            {
                return false;
            }
            db.Tickets.Add(ticket);
            db.SaveChanges();
            return true;
        }
        public bool CancelTicket(int ticketId)
        {
            Ticket foundTicket = db.Tickets.Where(x => x.TicketID == ticketId).FirstOrDefault();
            if (foundTicket == null)
            {
                return false;
            }
            foundTicket.Status = "CANCELLED";
            foundTicket.TotalCost = 0;
            db.Entry<Ticket>(foundTicket).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        public Ticket GetTicket(int ticketId)
        {
            return db.Tickets.Where(x => x.TicketID == ticketId).FirstOrDefault();
        }
        public List<Ticket> GetTicketByUserName(string username)
        {
            return db.Tickets.Where(x => x.UserName == username).ToList();
        }
    }
}
