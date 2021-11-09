using System;

namespace TheatreService.Exceptions
{
    public class TheatreAlreadExistsException : Exception
    {
        public TheatreAlreadExistsException() { }
        public TheatreAlreadExistsException(string message) : base(message) { }
    }
}
