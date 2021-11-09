using System;

namespace TicketService.Exceptions
{
    public class TicketCannotBeBookedException : Exception
    {
        public TicketCannotBeBookedException() { }
        public TicketCannotBeBookedException(string message) : base(message) { }
    }
}