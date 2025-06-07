using FundecodesApp.Data;
using FundecodesApp.Entities;

namespace FundecodesApp.Services
{
    public class UserVoluntarioService
    {
        private readonly UserVoluntarioRepository _repository;

        public UserVoluntarioService()
        {
            _repository = new UserVoluntarioRepository();
        }

        public List<UserVoluntario> GetAllUserVoluntarios()
        {
            return _repository.GetAll();
        }

        public UserVoluntario? GetUserVoluntarioById(string id)
        {
            return _repository.GetAll().FirstOrDefault(u => u.Id == id);
        }

        public void AddUserVoluntario(UserVoluntario newUserVoluntario)
        {
            var userVoluntarios = _repository.GetAll();
            userVoluntarios.Add(newUserVoluntario);
            _repository.SaveAll(userVoluntarios);
        }

        public bool UpdateUserVoluntario(string id, UserVoluntario updatedUserVoluntario)
        {
            var userVoluntarios = _repository.GetAll();
            var index = userVoluntarios.FindIndex(u => u.Id == id);

            if (index == -1)
                return false;

            userVoluntarios[index] = updatedUserVoluntario;
            _repository.SaveAll(userVoluntarios);
            return true;
        }

        public bool DeleteUserVoluntario(string id)
        {
            var userVoluntarios = _repository.GetAll();
            var userToRemove = userVoluntarios.FirstOrDefault(u => u.Id == id);

            if (userToRemove == null)
                return false;

            userVoluntarios.Remove(userToRemove);
            _repository.SaveAll(userVoluntarios);
            return true;
        }
    }
}
