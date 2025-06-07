using FundecodesApp.Data;
using FundecodesApp.Entities;

namespace FundecodesApp.Services
{
    public class UserAliadoService
    {
        private readonly UserAliadoRepository _repository;

        public UserAliadoService()
        {
            _repository = new UserAliadoRepository();
        }

        public List<UserAliado> GetAllUserAliados()
        {
            return _repository.GetAll();
        }

        public UserAliado? GetUserAliadoById(string id)
        {
            return _repository.GetAll().FirstOrDefault(u => u.Id == id);
        }

        public void AddUserAliado(UserAliado newUserAliado)
        {
            var userAliados = _repository.GetAll();
            userAliados.Add(newUserAliado);
            _repository.SaveAll(userAliados);
        }

        public bool UpdateUserAliado(string id, UserAliado updatedUserAliado)
        {
            var userAliados = _repository.GetAll();
            var index = userAliados.FindIndex(u => u.Id == id);

            if (index == -1)
                return false;

            userAliados[index] = updatedUserAliado;
            _repository.SaveAll(userAliados);
            return true;
        }

        public bool DeleteUserAliado(string id)
        {
            var userAliados = _repository.GetAll();
            var userToRemove = userAliados.FirstOrDefault(u => u.Id == id);

            if (userToRemove == null)
                return false;

            userAliados.Remove(userToRemove);
            _repository.SaveAll(userAliados);
            return true;
        }
    }
}
