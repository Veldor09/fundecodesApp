using FundecodesApp.Data;
using FundecodesApp.Entities;

namespace FundecodesApp.Services
{
    public class VoluntarioService
    {
        private readonly VoluntarioRepository _repository;

        public VoluntarioService()
        {
            _repository = new VoluntarioRepository();
        }

        public List<Voluntario> GetVoluntarios(bool includeHidden = false)
        {
            var voluntarios = _repository.GetAll();
            return includeHidden ? voluntarios : voluntarios.Where(v => !v.Hidden).ToList();
        }

        public Voluntario? GetVoluntarioById(string id)
        {
            return _repository.GetAll().FirstOrDefault(v => v.Id == id);
        }

        public void AddVoluntario(Voluntario voluntario)
        {
            var voluntarios = _repository.GetAll();
            voluntarios.Add(voluntario);
            _repository.SaveAll(voluntarios);
        }

        public void UpdateVoluntario(Voluntario updatedVoluntario)
        {
            var voluntarios = _repository.GetAll();
            var index = voluntarios.FindIndex(v => v.Id == updatedVoluntario.Id);
            if (index != -1)
            {
                voluntarios[index] = updatedVoluntario;
                _repository.SaveAll(voluntarios);
            }
        }

        public void HideVoluntario(string id)
        {
            var voluntarios = _repository.GetAll();
            var voluntario = voluntarios.FirstOrDefault(v => v.Id == id);
            if (voluntario != null)
            {
                voluntario.Hidden = true;
                _repository.SaveAll(voluntarios);
            }
        }
    }
}

