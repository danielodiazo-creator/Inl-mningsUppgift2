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
        List<Product> Products { get; set; }  //Lista av produkter (private)
        public double Total { get; set; }  //Total från orden
        public DateTime OrderDate {get; set;}  //Datum när orden skapades

        public static List<Order> ListOfOrders { get; set; } = new List<Order>();  //Statisk lista som sparar ordrana från alla användarna

        public void AddOrder(List<Product> products) // Metod för att skapa en ny order
        {
            var newOrder = new Order  //Vi skapar en ny objekt
            {
                Products = new List<Product>(products),  //Här kopierar man alla produkter som parameter
                Total = products.Sum(p => p.price), //Här kalkylerar man total priset av varje produkt
                OrderDate = DateTime.Now,  //Vi sparar det aktuella datumet
            };

            ListOfOrders.Add(newOrder); //Vi sparar orden i Listan av ordrarna
        }

        public void CheckOut() //Metoden för att slutföra köpet från kundvagnen
        {
            try
            {

                var choice = AnsiConsole.Prompt(      //Med hjälp av spectre.Console skapar vi en vall till användaren
                    new SelectionPrompt<string>()
                    .Title("[bold blue] Vill du slutföra ditt köp [/]")
                    .AddChoices("Ja", "Nej")


                    );

                if (choice == "Ja")  //Om användaren väljer Ja 
                {


                    double total = Product.Total(); //Vi räknar ut totalen med hjälp av metoden total
                    AnsiConsole.MarkupLine($"[bold blue] Total köpt: {Product.Total()} [/]");  // Vi visar totalen i konsolen

                    AddOrder(Product.shoppingCar); //Vi skapar en ny order med produkterna i kundvagnen
                    Product.shoppingCar.Clear();  //Vi tommer kundvagnen

                }
                else //Om användaren väljer Nej
                {
                    AnsiConsole.MarkupLine("[bold red] Köp avbrutet [/]");  // Vi visar i konsolen köp avbrutet
                }
            }
            catch (Exception error)
            {
                AnsiConsole.MarkupLine($"[red] Fel vid checkout: {error.Message}[/]");

            }
        }

        public static void ShowOrderHistory() //Statisk metod för att visa köp historiken
        {
            if(Order.ListOfOrders.Count == 0)  //Om det inte finns ordrar vi visar följande i konsolen
            {
                AnsiConsole.MarkupLine("[bold red]Inga tidigare köp hittades[/]");
                return;  // Vi avbryter metoden

            }

            else //Om det finns ordrar
            {
                foreach(var x in Order.ListOfOrders)  // Vi går igenom alla ordrar
                {
                    var table = new Table(); //Vi skapar en tabell för att visa ordrarna
                    
                    table.Title($"[bold green] Order från {x.OrderDate} [/]");

                    table.AddColumn("[bold yellow] produkt [/]");
                    table.AddColumn("[bold yellow] pris [/] "); 

                    foreach(var p in x.Products) //Vi adderar varje produkt till tabellen
                    {
                        table.AddRow(p.name, p.price.ToString("0,00")); //0.00 Står för typ av formaten som kommer visas i konsolen
                    }

                    table.AddRow($"[Bold yellow] Total [/]", $"[bold yellow]{x.Total: 0.00}[/]");
                    AnsiConsole.Write(table);  //Vi visar tabellen i konsolen
                    AnsiConsole.WriteLine();
                }
            }

        }

    }
}
