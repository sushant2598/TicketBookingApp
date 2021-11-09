using System;

namespace MovieService.Exceptions
{
    public class MovieIDAlreadyExists : Exception
    {
        public MovieIDAlreadyExists() { }
        public MovieIDAlreadyExists(string message) : base(message) { }
    }
}
