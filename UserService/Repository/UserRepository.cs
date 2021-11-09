using System.Collections.Generic;
using System.Linq;
using UserService.Context;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        public UserRepository(UserContext context)
        {
            this.context = context;
        }
        public bool RegisterUser(User user)
        {
            var dataFromDb = context.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();
            if (dataFromDb != null)
            {
                return false;
            }
            context.Users.Add(user);
            context.SaveChanges();
            return true;
        }
        public bool LoginUser(User user)
        {
            var dataFromDb = context.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();
            if (dataFromDb != null && dataFromDb.UserName == user.UserName && dataFromDb.Password == user.Password)
            {
                return true;
            }
            return false;
        }
        public bool UpdateUser(string username, string password, User user)
        {
            var dataFromDb = context.Users.Where(x => x.UserName == username).FirstOrDefault();
            if (dataFromDb != null && dataFromDb.Password == password)
            {
                dataFromDb.Name = user.Name;
                dataFromDb.Email = user.Email;
                dataFromDb.Contact = user.Contact;
                context.Entry<User>(dataFromDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateRoleofUser(string username, string role)
        {
            var dataFromDb = context.Users.Where(x => x.UserName == username).FirstOrDefault();
            if (dataFromDb != null)
            {
                dataFromDb.Role = role;
                context.Entry<User>(dataFromDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public User GetUserByUserName(string username)
        {
            return context.Users.Where(x => x.UserName == username).FirstOrDefault();
        }
        public List<User> GetUsers()
        {
            return context.Users.Where(x => true).ToList();
        }
    }
}
