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
            string[] lineSplit = line.Split(':');
            for(int i = 0; i < lineSplit.Length; i++)
            {
                if (i == 0) name = lineSplit[i];
                else password = lineSplit[i];
            }
        }

        public User(string name, string pass)
        {
            this.name = name;
            password = pass;
        }

        public string Output()
        {
            return string.Concat(name, ':', password);
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
            Console.WriteLine("Enter encryption key number");
            string key = Console.ReadLine();
            int keyInt = Convert.ToInt32(key);
            int keyLen = key.Length;
            StreamReader reader = new StreamReader("file.txt");
            List<User> users = new List<User>();

            while (!reader.EndOfStream)
            {
                string input = reader.ReadLine();
                //Console.WriteLine(input);
                char[] inputSplit = input.ToCharArray();
                char[] newString = new char[inputSplit.Length];

                for (int i = 0; i < inputSplit.Length; i++)
                {
                    int ascii = (int)inputSplit[i];
                    int newAscii = ascii - (Convert.ToInt32(key[i % keyLen]) - 48);
                    if (newAscii > 126)
                    {
                        newAscii += 94;
                    }
                    newString[i] = (char)newAscii;
                }
                string newStringString = new string(newString);
                //Console.WriteLine(newStringString);
                users.Add(new User(newStringString));
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
                        AddUser(ref users, key);
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
        public static void AddUser(ref List<User> users, string key)
        {
            Console.WriteLine("Enter username");
            string name = Console.ReadLine();
            Console.WriteLine("Enter password");
            string pass = Console.ReadLine();
            users.Add(new User(name, pass));
            Console.WriteLine("User Added!");
            StreamWriter writer = new StreamWriter("file.txt", true);
            string input = users[users.Count-1].Output();
            char[] inputSplit = input.ToCharArray();
            for (int i = 0; i < inputSplit.Length; i++)
            {
                int ascii = (int)inputSplit[i];
                int newAscii = ascii + Convert.ToInt32(key[i % key.Length]) - 48;
                if (newAscii > 126)
                {
                    newAscii -= 94;
                }
                //Console.WriteLine((char)newAscii);
                writer.Write((char)newAscii);
            }
            writer.WriteLine();
            writer.Close();
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
