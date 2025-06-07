using System.Text.Json;
using FundecodesApp.Entities;

namespace FundecodesApp.Data
{
    public class VoluntarioRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "voluntarios.json");
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true };

        public VoluntarioRepository()
        {
            if (!File.Exists(_filePath))
            {
                var initialData = new { voluntarios = new List<Voluntario>() };
                File.WriteAllText(_filePath, JsonSerializer.Serialize(initialData, _options));
            }
        }

        public List<Voluntario> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            var data = JsonSerializer.Deserialize<VoluntarioData>(json);
            return data?.Voluntarios ?? new List<Voluntario>();
        }

        public void SaveAll(List<Voluntario> voluntarios)
        {
            var data = new VoluntarioData { Voluntarios = voluntarios };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(data, _options));
        }

        private class VoluntarioData
        {
            public List<Voluntario> Voluntarios { get; set; } = new();
        }
    }
}
