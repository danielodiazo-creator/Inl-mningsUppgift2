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
        //Properties

        public string id { get; set; }      
        public string name { get; set; }
        public string category { get; set; }
        public double price { get; set; }
        public int stock { get; set; }

        

        public static List<Product> shoppingCar = new List<Product>(); //List av klass product shoppingCar. Den är static så att man kan
                                                                       //dela den bland alla instanster av product

        public override string ToString()         //Överride av toString så att man kan visa Product läsbart                   
        {
            return $"{name} - {price} SEK (Lager: {stock})";
        }

        public static void ShowProduct()    //Visar alla tillgängliga produkter som finns att välja på
        {
            
            if (User.LoggedInUser == null)   //Om det inte finns en inloggad användare 
            {
                User.LoginUser();              // Vi försöker att användare logga in här
                if(User.LoggedInUser == null) 
                {
                    AnsiConsole.MarkupLine("[red] Du måste skapa ett konto för att fortsätta [/]");
                    return;                  //Vi går ut från metoden med hjälp av return

                }

              
            }

            else                             //Om det finns en registrerad användare
            {
                string json = File.ReadAllText("produkter.json");   //Läser vi json filen med alla produkter som finns i den
                ProductRoot root = JsonSerializer.Deserialize<ProductRoot>(json);  //Deserialize Json filen till klassen producct root
                                                                                   //som innehåller alla produkterna
                var selectedProduct = AnsiConsole.Prompt(      
                    new SelectionPrompt<Product>()
                    .Title("[yellow] Välj bland de tillgängliga produkter[/]")           //Här skapas en urvarLista
                    .PageSize(10)                                                        
                    .MoreChoicesText("[bold grey] Visa mer [/]")
                    .AddChoices(root.produkter)
                    );


                shoppingCar.Add(selectedProduct);                       //Vi adderar den valde produkten till kundvagnen

                AnsiConsole.MarkupLine("[green]KundVagnen :[/]");
                AnsiConsole.MarkupLine($"ID: {selectedProduct.id}");              //Här visas information om produkterna i kundvagnen
                AnsiConsole.MarkupLine($"Namn: {selectedProduct.name}");
                AnsiConsole.MarkupLine($"Kategori: {selectedProduct.category}");
                AnsiConsole.MarkupLine($"Pris: {selectedProduct.price} SEK");
                AnsiConsole.MarkupLine($"Lager: {selectedProduct.stock}");

                AnsiConsole.MarkupLine("[bold yellow] Tryck på tangetbordet för att forsätta [/]");
                Console.ReadKey();

            }
  

        }


        public static void ShoppingCar()         //Metoden som visar kundvagnen
        {
            if(shoppingCar.Count == 0 || User.LoggedInUser == null)  //Om kundvagnen är tom eller användaren har inte loggad in
            {
                AnsiConsole.MarkupLine("[bold red] Kundvagnen är tom [/]");  //Visar vi detta meddelande
                return;                                                      //och vi går ut från metoden
                
            }

            var table = new Table();                  //Här skapar vi en tabell
            table.AddColumn("product");
            table.AddColumn("pris");

            foreach(var x in shoppingCar)    //Vi adderar en row för varje produkt i kundvagnen
            {
                table.AddRow(x.name, x.price.ToString("C2"));   //C2 betyder två decimaler som ska visas i vår tabell
            }

            AnsiConsole.Write(table);          //Här skrivs tabellen i konsolen

            AnsiConsole.MarkupLine("[bold green] Total pris: [/] " + Total() + "Sek"); //Vi visar totalpriset genom att kalla metoden Total()

            Order Payorder = new Order();  //Vi skapar en instans av klassen Order 
            Payorder.CheckOut();           //Och vi kallar metoden checkout för att slutföra köpet
        }

        public static double Total()      //Metoden visar sum av priserna
        {
           return shoppingCar.Sum(x => x.price);
        }






    }








    public class ProductRoot
    {
        public List<Product> produkter { get; set; }
    }

    
}
