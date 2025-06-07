using FundecodesApp.Data;
using FundecodesApp.Entities;

namespace FundecodesApp.Services
{
    public class AliadoService
    {
        private readonly AliadoRepository _repository;

        public AliadoService()
        {
            _repository = new AliadoRepository();
        }

        public List<Aliado> GetAliados(bool includeHidden = false)
        {
            var aliado = _repository.GetAll();
            return includeHidden ? aliado : aliado.Where(v => !v.Hidden).ToList();
        }

        public Aliado? GetAliadoById(string id)
        {
            return _repository.GetAll().FirstOrDefault(v => v.Id == id);
        }

        public void AddAliado(Aliado aliado)
        {
            var aliados = _repository.GetAll();
            aliados.Add(aliado);
            _repository.SaveAll(aliados);
        }

        public void UpdateAliado(Aliado updatedAliado)
        {
            var aliados = _repository.GetAll();
            var index = aliados.FindIndex(v => v.Id == updatedAliado.Id);
            if (index != -1)
            {
                aliados[index] = updatedAliado;
                _repository.SaveAll(aliados);
            }
        }

        public void HideAliado(string id)
        {
            var aliados = _repository.GetAll();
            var aliado = aliados.FirstOrDefault(v => v.Id == id);
            if (aliado != null)
            {
                aliado.Hidden = true;
                _repository.SaveAll(aliados);
            }
        }
    }
}
