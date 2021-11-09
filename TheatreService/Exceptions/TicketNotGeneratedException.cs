using System;

namespace TheatreService.Exceptions
{
    public class TicketNotGeneratedException : Exception
    {
        public TicketNotGeneratedException() { }
        public TicketNotGeneratedException(string message) : base(message) { }
    }
}
