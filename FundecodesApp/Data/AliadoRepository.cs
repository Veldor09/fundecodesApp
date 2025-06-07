using System.Text.Json;
using FundecodesApp.Entities;

namespace FundecodesApp.Data
{
    public class AliadoRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "aliados.json");
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true };

        public AliadoRepository()
        {
            if (!File.Exists(_filePath))
            {
                var initialData = new { aliados = new List<Aliado>() };
                File.WriteAllText(_filePath, JsonSerializer.Serialize(initialData, _options));
            }
        }

        public List<Aliado> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            var data = JsonSerializer.Deserialize<AliadoData>(json);
            return data?.Aliados ?? new List<Aliado>();
        }

        public void SaveAll(List<Aliado> aliados)
        {
            var data = new AliadoData { Aliados = aliados };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(data, _options));
        }

        private class AliadoData
        {
            public List<Aliado> Aliados { get; set; } = new();
        }
    }
}
