using Spectre.Console;
using System;
using System.Collections.Generic;
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

        public override string ToString()
        {
            return $"{name} - {price} SEK (Lager: {stock})";
        }

        public static void ShowProduct()
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

            AnsiConsole.MarkupLine("[green]Du valde:[/]");
            AnsiConsole.MarkupLine($"ID: {selectedProduct.id}");
            AnsiConsole.MarkupLine($"Namn: {selectedProduct.name}");
            AnsiConsole.MarkupLine($"Kategori: {selectedProduct.category}");
            AnsiConsole.MarkupLine($"Pris: {selectedProduct.price} SEK");
            AnsiConsole.MarkupLine($"Lager: {selectedProduct.stock}");

            Console.ReadKey();

            
           
  

        }

    }


    public class ProductRoot
    {
        public List<Product> produkter { get; set; }
    }
}
