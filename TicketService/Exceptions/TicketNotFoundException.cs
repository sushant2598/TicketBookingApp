using System;

namespace TicketService.Exceptions
{
    public class TicketNotFoundException : Exception
    {
        public TicketNotFoundException() { }
        public TicketNotFoundException(string message) : base(message) { }
    }
}