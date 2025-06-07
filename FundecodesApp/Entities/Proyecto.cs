namespace FundecodesApp.Entities
{
    public class Proyecto
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;

        public bool TieneFondos { get; set; } = false;
        public bool TieneAliados { get; set; } = false;
        public List<string> Aliados { get; set; } = new();

        public bool TieneVoluntarios { get; set; } = false;
        public List<string> Voluntarios { get; set; } = new();

        public bool Hidden { get; set; } = false;
    }
}
