using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2
{
    public class MenuHelper
    {
        public void Menu()
        {

            bool GoOn = true;
            while (GoOn)
            {

                AnsiConsole.MarkupLine("[blue bold] Hej och välkommen [/]");

                string option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Välj Alternativ")
                    .AddChoices("Skapa ny användare", "Logga in", "Lägga till order", "Spåra order", "Visa Produkter")
                    );

                switch (option)
                {
                    case "Skapa ny användare":
                        User.CreateNewUser();
                        break;

                    case "Logga in":
                        User.LoginUser();
                        break;

                    case "Lägg till order":

                        break;

                    case "Spåra order":

                        break;

                    case "Visa produkter":
                        Product.ShowProduct();


                        break;
                }

            }

        }
        


    }
}
