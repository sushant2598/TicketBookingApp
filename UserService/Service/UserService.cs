using System.Collections.Generic;
using UserService.Exceptions;
using UserService.Models;
using UserService.Repository;
using UserService.Shared;

namespace UserService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly EncryptionDecryption ed = new EncryptionDecryption();
        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }
        public bool RegisterUser(User user)
        {
            var data = repository.GetUserByUserName(user.UserName);
            if (data != null)
            {
                throw new UserNameAlreadyExistsException($"Username: {user.UserName} already taken");
            }
            if (PasswordPolicy.Valid(user.Password))
            {
                user.Role = user.Role.ToUpper();
                user.Password = EncryptionDecryption.Encrpt(user.Password);
                return repository.RegisterUser(user);
            }
            else
            {
                throw new WeakPasswordException("Password Strength is Weak");
            }
        }
        public bool LoginUser(User user)
        {
            user.Password = EncryptionDecryption.Encrpt(user.Password);
            var data = repository.GetUserByUserName(user.UserName);
            if (data == null)
            {
                throw new UserNotFoundException($"User with Username: {user.UserName} not found");
            }
            if (repository.LoginUser(user))
            {
                return true;
            }
            else
            {
                throw new InvalidCredentialsException("Invalid Credentials");
            }
        }
        public bool UpdateUser(string username, string password, User user)
        {
            var data = repository.GetUserByUserName(username);
            if (data != null)
            {
                password = EncryptionDecryption.Encrpt(password);
                user.Role = "CUSTOMER";
                return repository.UpdateUser(username, password, user);
            }
            throw new UserNotFoundException($"User with Username: {username} not found");
        }
        public bool UpdateRoleofUser(string username, string role)
        {
            var foundUser = repository.GetUserByUserName(username);
            if (foundUser == null)
            {
                throw new UserNotFoundException($"User with Username: {username} not found");
            }
            if (foundUser.Role == "ADMIN")
            {
                throw new System.Exception($"The Role of {username} cannot be changed");
            }
            role = role.ToUpper();
            return repository.UpdateRoleofUser(username, role);
        }
        public User GetUserByUserName(string username)
        {
            var foundUser = repository.GetUserByUserName(username);
            if (foundUser == null)
            {
                throw new UserNotFoundException($"User with Username: {username} not found");
            }
            return foundUser;
        }
        public List<User> GetUsers()
        {
            var foundUsers = repository.GetUsers();
            if (foundUsers.Count == 0)
            {
                throw new UserNotFoundException("No User is Present");
            }
            return foundUsers;
        }
    }
}
