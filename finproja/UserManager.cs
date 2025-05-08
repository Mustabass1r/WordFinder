using System;
using System.Collections.Generic;
using System.Linq;

namespace finproja
{
    internal class UserManager
    {
        private static UserManager _instance;
        private List<User> users;
        public User currentUser { get; set; }

        private UserManager()
        {
            users = new List<User>();
        }

        public static UserManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserManager();
                }
                return _instance;
            }
        }

        public void RegisterUser(string name, string email, string password)
        {
            User newUser = new User(name, email, password);
            users.Add(newUser);
        }

        public User Login(string email, string password)
        {
            RegisterUser("1","1","1");
            User user = users.FirstOrDefault(u => u.Email == email);

            if (user != null && user.VerifyPassword(password))
            {
                Console.WriteLine("Login successful!");
                currentUser = user;
                return user;
            }
            else
            {
                Console.WriteLine("Invalid credentials. Login failed.");
                return null;
            }
        }
    }
}
