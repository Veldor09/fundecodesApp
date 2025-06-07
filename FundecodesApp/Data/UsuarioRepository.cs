using System.Text.Json;
using FundecodesApp.Entities;

namespace FundecodesApp.Data
{
    public class UsuarioRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "usuarios.json");
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true };

        public UsuarioRepository()
        {
            if (!File.Exists(_filePath))
            {
                var initialData = new { usuarios = new List<Usuario>() };
                File.WriteAllText(_filePath, JsonSerializer.Serialize(initialData, _options));
            }
        }

        public List<Usuario> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            var data = JsonSerializer.Deserialize<UsuarioData>(json);
            return data?.Usuarios ?? new List<Usuario>();
        }

        public void SaveAll(List<Usuario> usuarios)
        {
            var data = new UsuarioData { Usuarios = usuarios };
            File.WriteAllText(_filePath, JsonSerializer.Serialize(data, _options));
        }

        public Usuario? GetByCorreo(string correo)
        {
            return GetAll().FirstOrDefault(u => u.Email == correo);
        }

        private class UsuarioData
        {
            public List<Usuario> Usuarios { get; set; } = new();
        }
    }
}
