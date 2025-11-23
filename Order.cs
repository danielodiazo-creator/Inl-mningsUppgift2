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
        List<Product> Products { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate {get; set;}

        public static List<Order> ListOfOrders { get; set; } = new List<Order>();

        public void AddOrder(List<Product> products)
        {
            var newOrder = new Order
            {
                Products = new List<Product>(products),
                Total = products.Sum(p => p.price),
                OrderDate = DateTime.Now,
            };

            ListOfOrders.Add(newOrder);
        }

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

                AddOrder(Product.shoppingCar);
                Product.shoppingCar.Clear();

            }
            else
            {
                AnsiConsole.MarkupLine("[bold red] Köp avbrutet [/]");
            }

        }

        public static void ShowOrderHistory()
        {
            if(Order.ListOfOrders.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red]Inga tidigare köp hittades[/]");
                return;

            }

            else
            {
                foreach(var x in Order.ListOfOrders)
                {
                    var table = new Table();
                    
                    table.Title($"[bold green] Order från {x.OrderDate} [/]");

                    table.AddColumn("[bold yellow] produkt [/]");
                    table.AddColumn("[bold yellow] pris [/] "); 

                    foreach(var p in x.Products)
                    {
                        table.AddRow(p.name, p.price.ToString("0,00"));
                    }

                    table.AddRow($"[Bold yellow] Total [/]", $"[bold yellow]{x.Total: 0.00}[/]");
                    AnsiConsole.Write(table);

                }
            }

        }

    }
}
