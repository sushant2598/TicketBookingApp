using System;

namespace UserService.Exceptions
{
    public class UserNameAlreadyExistsException : Exception
    {
        public UserNameAlreadyExistsException() { }
        public UserNameAlreadyExistsException(string message) : base(message) { }
    }
}
