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

        
        public static void ShowProduct()
        {


            string json = File.ReadAllText("produkter.json");
            ProductRoot root = JsonSerializer.Deserialize<ProductRoot>(json);

            AnsiConsole.MarkupLine("[yellow] tillgängliga produkter [/]");

            foreach(var x in root.produkter)
            {
                Console.WriteLine($"{x.id}. {x.name} ({x.category}) - {x.price} SEK - Lager: {x.stock}");
            }
            

        }
    }

    public class ProductRoot
    {
        public List<Product> produkter { get; set; }
    }
}
