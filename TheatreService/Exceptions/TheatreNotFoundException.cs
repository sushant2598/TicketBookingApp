using System;

namespace TheatreService.Exceptions
{
    public class TheatreNotFoundException : Exception
    {
        public TheatreNotFoundException() { }
        public TheatreNotFoundException(string message) : base(message) { }
    }
}
