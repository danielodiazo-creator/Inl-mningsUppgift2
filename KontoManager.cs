using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InlämningsUppgift2
{
    public static class KontoManager<T>  //T står för Type
    {
        public static List<T> Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            string jsonText = File.ReadAllText(filePath);

            List<T> items = JsonSerializer.Deserialize<List<T>>(jsonText);
            
            if(items == null)
            {
                return new List<T>();
            }

            else
            {
                return items;
            }

        }

        public static void Save(List<T> items, string filePath)
        {
            string jsonText = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonText);
           

        }


    }
}
