using System.ComponentModel.DataAnnotations;

namespace TicketService.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        public string ImdbID { get; set; }
        public int TheatreID { get; set; }
        public string ShowDate { get; set; }
        public string ShowTimings { get; set; }
        public int NoOfTickets { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public int TotalCost { get; set; }
    }
}
