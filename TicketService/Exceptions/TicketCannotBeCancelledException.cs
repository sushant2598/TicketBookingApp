using System;

namespace TicketService.Exceptions
{
    public class TicketCannotBeCancelledException : Exception
    {
        public TicketCannotBeCancelledException() { }
        public TicketCannotBeCancelledException(string message) : base(message) { }
    }
}
