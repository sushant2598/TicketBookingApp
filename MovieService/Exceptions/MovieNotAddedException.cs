using System;

namespace MovieService.Exceptions
{
    public class MovieNotAddedException : Exception
    {
        public MovieNotAddedException() { }
        public MovieNotAddedException(string message) : base(message) { }
    }
}
