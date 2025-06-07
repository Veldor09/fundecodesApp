using System.Text.Json;
using FundecodesApp.Entities;

namespace FundecodesApp.Data
{
    public class ProyectoRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "proyectos.json");
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true };

        public ProyectoRepository()
        {
            if (!File.Exists(_filePath))
            {
                var initialData = new ProyectoData();
                File.WriteAllText(_filePath, JsonSerializer.Serialize(initialData, _options));
            }
        }

        public List<Proyecto> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            var data = JsonSerializer.Deserialize<ProyectoData>(json);
            return data?.Proyectos ?? new List<Proyecto>();
        }

        public void SaveAll(List<Proyecto> proyectos)
        {
            var data = new ProyectoData { Proyectos = proyectos };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(data, _options));
        }

        private class ProyectoData
        {
            public List<Proyecto> Proyectos { get; set; } = new();
        }
    }
}
