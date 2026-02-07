using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace PedidoApp.Services
{
    public static class JsonDatabase
    {
        private static string pasta = "Data";

        public static List<T> Load<T>(string fileName)
        {
            string path = Path.Combine(pasta, fileName);

            if (!File.Exists(path))
                return new List<T>();

            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<T>>(json)
                   ?? new List<T>();
        }

        public static void Save<T>(string fileName, List<T> data)
        {
            string path = Path.Combine(pasta, fileName);

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(path, json);
        }
    }
}
