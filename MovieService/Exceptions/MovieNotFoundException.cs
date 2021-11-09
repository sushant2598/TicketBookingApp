using System;

namespace MovieService.Exceptions
{
    public class MovieNotFoundException : Exception
    {
        public MovieNotFoundException() { }
        public MovieNotFoundException(string message) : base(message) { }
    }
}
