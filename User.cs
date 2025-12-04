using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using System.Text.Json;
using System.Globalization;

namespace InlämningsUppgift2
{
    public class User  //Klass som representerar användaren
    {
        //Properties från avändaren

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public int KundNummer { get; set; } //Användares Unika nummer


        public static int KundCounter = 0; //Statisk Räknare för att skapa unika kundNummer

        private static string filePath = "users.json"; //här sparar man alla användare

        public static User LoggedInUser = null; //Representerar inloggad användare, annars börjar den som null. 


        public User(string username, string password, string email, string adress) //Här är konstruktören som skapar en användare och ger 
        {                                                                          //den en unik kundnummer
            KundNummer = KundCounter; //Här tilldelar man den nuvarande nummer
            KundCounter++;           //Och här ökar man räknare till nästa användare
            Username = username;
            Password = password;
            Email = email;
            Adress = adress;

        }

        public static User CreateNewUser() //Metoden för att skapa en ny användare med hjälp av konsolen
        {
            string username = AnsiConsole.Ask<string>("Ange ditt namn");
            string password = AnsiConsole.Ask<string>("Ange ditt lösenord");
            string email = AnsiConsole.Ask<string>("Ange ditt email");
            string adress = AnsiConsole.Ask<string>("Ange ditt adress");

            User newUser = new User(username, password, email, adress); //Här skapar man en instans av User med det hämtade data
            SaveUser(newUser);  //Här sparar man användaren i minnet
            return newUser;    //Och här returnerar man den


        }

        public static void SaveUser(User user) //Här sparar man användare
        {

            try
            {
                List<User> allUsers = LoadUsers(); //Vi hämtar alla användare från filen
                allUsers.Add(user); //Vi adderar den nya användaren till listan

                KontoManager<User>.Save(allUsers, filePath); //här sparar man hela listan med hjälp av metoden Save från klassen
                                                             //KontoManager

            }

            catch (Exception error)
            {
                AnsiConsole.MarkupLine($"[red] Kunde inte spara användaren: {error.Message}[/]");
            }


        }                                                

        public static List<User> LoadUsers()  //Hämtar användares lista från filen 
        {
            try
            {

                List<User> allUsers = KontoManager<User>.Load(filePath); //Det här kallar metoden Load från klassen Kontomanager som 
                                                                         //läser json filen och deserialize listan

                if (allUsers != null && allUsers.Count > 0)           //Om det finns användare, uppdaterar man här KundCounter för att unvdika problem
                {
                    int highestNumber = allUsers.Max(x => x.KundNummer); //Här hittar man högsta nummer från den nuvarande användare
                    KundCounter = highestNumber + 1; //vi faställer räknaren till nästa tillgängligt nummer 
                }

                return allUsers; //Retunerar listan, den kan vara också tom
            }
               
            
            catch (Exception error)
            {
                AnsiConsole.MarkupLine($"[red] Ett oväntat fel inträffade: {error.Message}[/]");
                return new List<User>();
            }



        }

        public static User LoginUser() //Metoden som begär samma uppgifter för att logga in
        {
            List<User> allUsers = LoadUsers(); //Här hämtar man existerande användare

            if(allUsers.Count == 0) //Om det inste finns användare, begär vi här att skapa ett konto
            {
                AnsiConsole.MarkupLine("Du måste först skapa ett konto");
                return CreateNewUser();
            }

            //Vi begär uppggifter från användare för att kolla om de matchar 
            string username = AnsiConsole.Ask<string>("Ange ditt användarnamn:");
            string password = AnsiConsole.Ask<string>("Ange ditt lösenord:");
            

            User foundUser = null;

            foreach(User x in allUsers)  //Här söker man i användarnas lista om det finns en användare som matchar med samma uppgifter 
            {
                if(x.Username == username && x.Password == password)
                {
                    foundUser = x;

                }
            }

            if(foundUser == null) //Om det inte hittades 
            {
                AnsiConsole.MarkupLine("[red] Fel användarnman [/]"); //Skriver man ett meddelande
                return null;  // Och returnerar man null
            }

            //Om det däremot hittades
            
            AnsiConsole.MarkupLine("[green] Du har loggat in [/]" + foundUser.Username); //Skriver vi ett meddelande som notis
            LoggedInUser = foundUser; //Faställer vi LoggedInUser
            return foundUser;  //Och returnerar den
        }


        public static void DeleteAccount()
        {
            if (LoggedInUser == null)
            {
                AnsiConsole.MarkupLine("[bold red] Du måste logga in först [/]");
                return;
            }

            var confirm = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[bold red] Är du säker på att du vill radera ditt konto[/]")
                .AddChoices("Ja", "Nej")
                );

            if(confirm == "Ja")
            {
                try
                {

                    List<User> allUsers = LoadUsers();
                    User userToDelete = null;

                    foreach(User x in allUsers)
                    {
                        if(x.Username == LoggedInUser.Username && x.Password == LoggedInUser.Password)
                        {
                            userToDelete = x;
                            break;

                        }

                    }

                    if (userToDelete != null)
                    {
                        allUsers.Remove(userToDelete);
                        KontoManager<User>.Save(allUsers, filePath);

                        AnsiConsole.MarkupLine("[bold green] Ditt konto har raderats [/]");
                        LoggedInUser = null;
                    }

                    else
                    {
                        AnsiConsole.MarkupLine("Användaren har inte hittats");
                    }

                }


                catch(Exception error)
                {
                    AnsiConsole.MarkupLine("[bold red] Ett fel inträffade när kontot skulle tas bort");
                }


            }







        }



    }
}
