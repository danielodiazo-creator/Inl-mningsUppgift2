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
            AnsiConsole.MarkupLine("[blue bold] Hej och välkommen [/]"); //Vi välkomnar användare med ett meddelande


            bool GoOn = true;  //En loop så ska upprepas så länge GoOn är true
            while (GoOn)
            {

               
                string option = AnsiConsole.Prompt(  //En lista av möjliga val för användaren
                    new SelectionPrompt<string>()
                    .Title("Välj Alternativ")
                    .AddChoices("Skapa ny användare", "Logga in", "Spåra order", "Lägga till order", "Visa Kundvagnen")
                    );

                switch (option)    //Beroende på valet så kallar vi de följande metoderna
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

                    case "Visa Kundvagnen":
                        Product.ShoppingCar();
                        break;

                    case "Spåra order":
                        Order.ShowOrderHistory();
                        break;

                    
                }

            }

        }
        


    }
}
