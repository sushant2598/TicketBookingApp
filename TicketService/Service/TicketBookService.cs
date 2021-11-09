using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TicketService.Models;

namespace TicketService.Service
{
    public class TicketBookService : ITicketBookService
    {
        static readonly HttpClient client = new HttpClient();
        public async Task<int> BookTicket(Ticket ticket, string[] token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token[0], token[1]);
            //HttpResponseMessage response = await client.GetAsync($"https://localhost:44301/api/Theatre/book/{ticket.ImdbID}/{ticket.TheatreID}/{ticket.ShowDate}/{ticket.ShowTimings}/{ticket.NoOfTickets}");
            HttpResponseMessage response = await client.GetAsync($"http://theatre-service:80/api/Theatre/book/{ticket.ImdbID}/{ticket.TheatreID}/{ticket.ShowDate}/{ticket.ShowTimings}/{ticket.NoOfTickets}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            int ticketBook = JsonConvert.DeserializeObject<int>(responseBody);
            return ticketBook;
        }

        public async Task<bool> CancelTicket(Ticket ticket, string[] token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token[0], token[1]);
            //HttpResponseMessage response = await client.GetAsync($"https://localhost:44301/api/Theatre/cancel/{ticket.ImdbID}/{ticket.TheatreID}/{ticket.ShowDate}/{ticket.ShowTimings}/{ticket.NoOfTickets}");
            HttpResponseMessage response = await client.GetAsync($"http://theatre-service:80/api/Theatre/cancel/{ticket.ImdbID}/{ticket.TheatreID}/{ticket.ShowDate}/{ticket.ShowTimings}/{ticket.NoOfTickets}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            bool ticketCancel = JsonConvert.DeserializeObject<bool>(responseBody);
            return ticketCancel;
        }
    }
}
