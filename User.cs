using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using System.Text.Json;

namespace InlämningsUppgift2
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public int KundNummer { get; set; } //Unik användares nummer


        public static int KundCounter = 100;

        private static string filePath = "users.json";


        public User(string username, string password, string email, string adress)
        {
            KundNummer = KundCounter;
            KundCounter++;
            Username = username;
            Password = password;
            Email = email;
            Adress = adress;

        }

        public static User CreateNewUser()
        {
            string username = AnsiConsole.Ask<string>("Ange ditt namn");
            string password = AnsiConsole.Ask<string>("Ange ditt lösenord");
            string email = AnsiConsole.Ask<string>("Ange ditt email");
            string adress = AnsiConsole.Ask<string>("Ange ditt adress");

            User newUser = new User(username, password, email, adress);
            SaveUser(newUser);
            return newUser;


        }

        public static void SaveUser(User user)
        {
            List<User> allUsers = LoadUsers();
            allUsers.Add(user);

            string jsonText = JsonSerializer.Serialize(allUsers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonText);
        }

        public static List<User> LoadUsers()
        {
            if (!File.Exists(filePath))
            {
                return new List<User>();
            }

            string jsonText = File.ReadAllText(filePath);
            List<User> allUsers = JsonSerializer.Deserialize<List<User>>(jsonText);

            if (allUsers != null && allUsers.Count > 0)
            {
                int highestNumber = 0;
                foreach (User x in allUsers)
                {
                    if (x.KundNummer > highestNumber)
                    {
                        highestNumber = x.KundNummer;
                    }
                }
                KundCounter = highestNumber + 1;
                return allUsers;
            }

            return new List<User>();
        }



    }
}
