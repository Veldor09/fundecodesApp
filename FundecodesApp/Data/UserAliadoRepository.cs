using System.Text.Json;
using FundecodesApp.Entities;

namespace FundecodesApp.Data
{
    public class UserAliadoRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "useraliados.json");
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true };

        public UserAliadoRepository()
        {
            if (!File.Exists(_filePath))
            {
                var initialData = new { userAliados = new List<UserAliado>() };
                File.WriteAllText(_filePath, JsonSerializer.Serialize(initialData, _options));
            }
        }

        public List<UserAliado> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            var data = JsonSerializer.Deserialize<UserAliadoData>(json);
            return data?.UserAliados ?? new List<UserAliado>();
        }

        public void SaveAll(List<UserAliado> userAliados)
        {
            var data = new UserAliadoData { UserAliados = userAliados };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(data, _options));
        }

        private class UserAliadoData
        {
            public List<UserAliado> UserAliados { get; set; } = new();
        }
    }
}

