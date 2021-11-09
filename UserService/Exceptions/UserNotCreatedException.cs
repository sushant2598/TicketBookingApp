using System;

namespace UserService.Exceptions
{
    public class UserNotCreatedException : Exception
    {
        public UserNotCreatedException() { }
        public UserNotCreatedException(string message) : base(message) { }
    }
}
