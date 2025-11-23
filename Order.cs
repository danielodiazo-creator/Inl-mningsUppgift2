using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlämningsUppgift2
{
    public class Order
    {
       


        public void CheckOut()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[bold blue] Vill du slutföra ditt köp [/]")
                .AddChoices("Ja", "Nej")


                );

            if (choice == "Ja")
            {
                double total = Product.Total();
                AnsiConsole.MarkupLine($"[bold blue] Total köpt: {Product.Total()} [/]");
                Product.shoppingCar.Clear();

            }
            else
            {
                AnsiConsole.MarkupLine("[bold red] Köp avbruten [/]");
            }

        }

        

    }
}
