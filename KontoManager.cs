using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InlämningsUppgift2
{
    public static class KontoManager<T>  //Generisk klasss som tillåter oss använda vilken som helst typ av obejkt T
    {
        public static List<T> Load(string filePath) //Metoden för att hämta ut data från en json file
        {
            if (!File.Exists(filePath))  // Kontrollerar om filen existerar. Om den inte finns returnerar en tom List
            {
                return new List<T>();
            }

            string jsonText = File.ReadAllText(filePath); // Läser json filen som en text

            List<T> items = JsonSerializer.Deserialize<List<T>>(jsonText);   //Deserialize texten json till en list av objects från type T
            
            if(items == null) //Om deserializen är null returnerar en tom lista
            {
                return new List<T>();
            }

            else  //Om allt funkar bra så returnerar den listan med alla objekt
            {
                return items;
            }

        }

        public static void Save(List<T> items, string filePath)  // Metoden för att spara data till en json file
        {

            //Konverterar listan av objekt till en text file med writeIndented format som för det lättare att läsa

            string jsonText = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
           
            File.WriteAllText(filePath, jsonText);  //Skriver json texten i filen, om filen inte finns den kommer att skapa den
           

        }


    }
}
