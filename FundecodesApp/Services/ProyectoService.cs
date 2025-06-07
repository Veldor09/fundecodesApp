using FundecodesApp.Data;
using FundecodesApp.Entities;

namespace FundecodesApp.Services
{
    public class ProyectoService
    {
        private readonly ProyectoRepository _repository;

        public ProyectoService()
        {
            _repository = new ProyectoRepository();
        }

        public List<Proyecto> GetProyectos(bool includeHidden = false)
        {
            var proyectos = _repository.GetAll();
            return includeHidden ? proyectos : proyectos.Where(p => !p.Hidden).ToList();
        }

        public Proyecto? GetProyectoById(string id)
        {
            return _repository.GetAll().FirstOrDefault(p => p.Id == id);
        }

        public void AddProyecto(Proyecto proyecto)
        {
            var proyectos = _repository.GetAll();
            proyectos.Add(proyecto);
            _repository.SaveAll(proyectos);
        }

        public void UpdateProyecto(Proyecto updatedProyecto)
        {
            var proyectos = _repository.GetAll();
            var index = proyectos.FindIndex(p => p.Id == updatedProyecto.Id);
            if (index != -1)
            {
                proyectos[index] = updatedProyecto;
                _repository.SaveAll(proyectos);
            }
        }

        public void HideProyecto(string id)
        {
            var proyectos = _repository.GetAll();
            var proyecto = proyectos.FirstOrDefault(p => p.Id == id);
            if (proyecto != null)
            {
                proyecto.Hidden = true;
                _repository.SaveAll(proyectos);
            }
        }
    }
}
