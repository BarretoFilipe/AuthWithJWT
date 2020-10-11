using Server.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Server.API.Repositories
{
    public static class UserRepository
    {
        private static List<User> Users => new List<User>
            {
                new User(1, "Barreto", "barreto@email.com", "123456"),
                new User(2, "Cristianinha", "cristianinha@email.com", "654321"),
                new User(3, "Fabinho Gameplay", "fabinho@email.com", "jordan23")
            };

        public static User GetUser(string email, string password)
        {
            return Users.Where(u => u.Email == email.ToLower() && u.Password == password).FirstOrDefault();
        }

        public static List<User> GetUsers()
        {
            return Users;
        }
    }
}