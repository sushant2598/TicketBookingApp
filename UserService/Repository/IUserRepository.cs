using System.Collections.Generic;
using UserService.Models;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        bool RegisterUser(User user);
        bool LoginUser(User user);
        bool UpdateUser(string username, string password, User user);
        bool UpdateRoleofUser(string username, string role);
        User GetUserByUserName(string username);
        List<User> GetUsers();
    }
}
