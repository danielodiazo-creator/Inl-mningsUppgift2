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
            AnsiConsole.MarkupLine("[blue bold] Hej och välkommen [/]");


            bool GoOn = true;
            while (GoOn)
            {

               
                string option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Välj Alternativ")
                    .AddChoices("Skapa ny användare", "Logga in", "Spåra order", "Lägga till order")
                    );

                switch (option)
                {
                    case "Skapa ny användare":
                        User.CreateNewUser();
                        break;

                    case "Logga in":
                        User.LoginUser();
                        break;

                    case "Lägga till order":
                        Product.ShowProduct();
                        break;

                    case "Spåra order":

                        break;

                    
                }

            }

        }
        


    }
}
