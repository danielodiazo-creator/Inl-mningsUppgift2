using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InlämningsUppgift2
{
    public class Product
    {
        public string id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double price { get; set; }
        public int stock { get; set; }

        

        public static List<Product> shoppingCar = new List<Product>();

        public override string ToString()
        {
            return $"{name} - {price} SEK (Lager: {stock})";
        }

        public static void ShowProduct()
        {
            
            if (User.LoggedInUser == null)
            {
                User.LoginUser();
                if(User.LoggedInUser == null) 
                {
                    AnsiConsole.MarkupLine("[red] Du måste skapa ett konto för att fortsätta [/]");
                    return;

                }

              
            }

            else
            {
                string json = File.ReadAllText("produkter.json");
                ProductRoot root = JsonSerializer.Deserialize<ProductRoot>(json);

                var selectedProduct = AnsiConsole.Prompt(
                    new SelectionPrompt<Product>()
                    .Title("[yellow] Välj bland de tillgängliga produkter[/]")
                    .PageSize(10)
                    .MoreChoicesText("[bold grey] Visa mer [/]")
                    .AddChoices(root.produkter)
                    );


                shoppingCar.Add(selectedProduct);

                AnsiConsole.MarkupLine("[green]KundVagnen :[/]");
                AnsiConsole.MarkupLine($"ID: {selectedProduct.id}");
                AnsiConsole.MarkupLine($"Namn: {selectedProduct.name}");
                AnsiConsole.MarkupLine($"Kategori: {selectedProduct.category}");
                AnsiConsole.MarkupLine($"Pris: {selectedProduct.price} SEK");
                AnsiConsole.MarkupLine($"Lager: {selectedProduct.stock}");

                Console.ReadKey();

            }
  

        }


        public static void ShoppingCar()
        {
            if(shoppingCar.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold red] Kundvagnen är tom");
                
            }

            var table = new Table();
            table.AddColumn("product");
            table.AddColumn("pris");

            foreach(var x in shoppingCar)
            {
                table.AddRow(x.name, x.price.ToString("C2"));
            }

            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("[bold green] Total pris: [/] " + Total() + "Sek");

            Order Payorder = new Order();
            Payorder.CheckOut();
        }

        public static double Total()
        {
           return shoppingCar.Sum(x => x.price);
        }


    }


    public class ProductRoot
    {
        public List<Product> produkter { get; set; }
    }

    
}
