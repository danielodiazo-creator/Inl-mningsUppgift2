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


        public static int KundCounter = 0;

        private static string filePath = "users.json";

        public static User LoggedInUser = null;


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

            KontoManager<User>.Save(allUsers, filePath);
        }

        public static List<User> LoadUsers()
        {
            List<User> allUsers = KontoManager<User>.Load(filePath);
            
            if(allUsers != null && allUsers.Count > 0)
            {
                int highestNumber = allUsers.Max(x => x.KundNummer);
                KundCounter = highestNumber + 1;
            }

            return allUsers;
            
        }

        public static User LoginUser()
        {
            List<User> allUsers = LoadUsers();

            if(allUsers.Count == 0)
            {
                AnsiConsole.MarkupLine("Du måste skapa ett konto först");
                return CreateNewUser();
            }

            
            string username = AnsiConsole.Ask<string>("Ange ditt användarnamn:");
            string password = AnsiConsole.Ask<string>("Ange ditt lösenord:");
            

            User foundUser = null;

            foreach(User x in allUsers)
            {
                if(x.Username == username && x.Password == password)
                {
                    foundUser = x;

                }
            }

            if(foundUser == null)
            {
                AnsiConsole.MarkupLine("[red] Fel användarnman [/]");
                return null;
            }

            AnsiConsole.MarkupLine("[green] Du har loggat in [/]" + foundUser.Username);
            LoggedInUser = foundUser;
            return foundUser;
        }


    }
}
