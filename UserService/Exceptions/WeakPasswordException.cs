using System;

namespace UserService.Exceptions
{
    public class WeakPasswordException : Exception
    {
        public WeakPasswordException() { }
        public WeakPasswordException(string message) : base(message) { }
    }
}
