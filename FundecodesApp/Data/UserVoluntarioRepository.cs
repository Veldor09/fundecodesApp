using System.Text.Json;
using FundecodesApp.Entities;

namespace FundecodesApp.Data
{
    public class UserVoluntarioRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "uservoluntarios.json");
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true };

        public UserVoluntarioRepository()
        {
            if (!File.Exists(_filePath))
            {
                var initialData = new { uservoluntarios = new List<UserVoluntario>() };
                File.WriteAllText(_filePath, JsonSerializer.Serialize(initialData, _options));
            }
        }

        public List<UserVoluntario> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            var data = JsonSerializer.Deserialize<UserVoluntarioData>(json);
            return data?.UserVoluntarios ?? new List<UserVoluntario>();
        }

        public void SaveAll(List<UserVoluntario> userVoluntarios)
        {
            var data = new UserVoluntarioData { UserVoluntarios = userVoluntarios };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(data, _options));
        }

        private class UserVoluntarioData
        {
            public List<UserVoluntario> UserVoluntarios { get; set; } = new();
        }
    }
}
