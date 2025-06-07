using FundecodesApp.Data;
using FundecodesApp.Entities;

namespace FundecodesApp.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;

        public UsuarioService()
        {
            _repository = new UsuarioRepository();
        }

        public List<Usuario> GetUsuarios(bool includeHidden = false)
        {
            var usuarios = _repository.GetAll();
            return includeHidden ? usuarios : usuarios.Where(u => !u.Hidden).ToList();
        }

        public Usuario? GetUsuarioById(string id)
        {
            return _repository.GetAll().FirstOrDefault(u => u.Id == id);
        }

        public void AddUsuario(Usuario usuario)
        {
            var usuarios = _repository.GetAll();
            usuarios.Add(usuario);
            _repository.SaveAll(usuarios);
        }

        public void UpdateUsuario(Usuario updatedUsuario)
        {
            var usuarios = _repository.GetAll();
            var index = usuarios.FindIndex(u => u.Id == updatedUsuario.Id);
            if (index != -1)
            {
                usuarios[index] = updatedUsuario;
                _repository.SaveAll(usuarios);
            }
        }

        public void HideUsuario(string id)
        {
            var usuarios = _repository.GetAll();
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                usuario.Hidden = true;
                _repository.SaveAll(usuarios);
            }
        }


    }
}
