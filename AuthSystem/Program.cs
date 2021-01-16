using System;
using System.Collections.Generic;
using System.IO;

namespace AuthSystem
{
    class User
    {
        public string name;
        private string password;

        public User(string line)
        {
            name = line.Split(':')[0];
            password = line.Split(':')[1];
        }
        public User(string name, string pass)
        {
            this.name = name;
            password = pass;
        }

        public bool CheckPassword(string user, string password)
        {
            if(user == name && password == this.password)
            {
                return true;
            }
            return false;
        }

    }
    class MainClass
    {
        public static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("file.txt");
            List<User> users = new List<User>();

            while (!reader.EndOfStream)
            {
                users.Add(new User(reader.ReadLine()));
            }

            reader.Close();

            bool login = false;
            while (!login)
            {
                Console.WriteLine("---");
                Console.WriteLine("1: Add new user");
                Console.WriteLine("2: Login");

                switch (Console.ReadLine()[0])
                {
                    case '1':
                        AddUser(ref users);
                        break;
                    case '2':
                        login = Login(users);
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }

            }

        }
        public static void AddUser(ref List<User> users)
        {
            Console.WriteLine("Enter username");
            string name = Console.ReadLine();
            Console.WriteLine("Enter password");
            string pass = Console.ReadLine();
            users.Add(new User(name, pass));
            Console.WriteLine("User Added!");
        }
        public static bool Login(List<User> users)
        {
            bool login = false;
            while (!login)
            {
                Console.WriteLine("Enter username");
                string name = Console.ReadLine();
                Console.WriteLine("Enter password");
                string pass = Console.ReadLine();
                foreach(User user in users)
                {
                    login = user.CheckPassword(name, pass);
                    if (login)
                    {
                        Console.WriteLine("Login successful!");
                        return true;
                    }
                }
                Console.WriteLine("Username / Password not found.");
                return false;
            }

            return false;
        }
    }
}
