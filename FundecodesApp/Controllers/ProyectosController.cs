using Microsoft.AspNetCore.Mvc;
using FundecodesApp.Entities;
using FundecodesApp.Services;

namespace FundecodesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectosController : ControllerBase
    {
        private readonly ProyectoService _service;

        public ProyectosController()
        {
            _service = new ProyectoService();
        }

        [HttpGet]
        public IActionResult GetProyectos([FromQuery] bool includeHidden = false)
        {
            var proyectos = _service.GetProyectos(includeHidden);
            return Ok(proyectos);
        }

        [HttpGet("{id}")]
        public IActionResult GetProyecto(string id)
        {
            var proyecto = _service.GetProyectoById(id);
            if (proyecto == null)
                return NotFound($"Proyecto con ID {id} no encontrado.");
            return Ok(proyecto);
        }

        [HttpPost]
        public IActionResult AddProyecto([FromBody] Proyecto proyecto)
        {
            if (proyecto == null || string.IsNullOrWhiteSpace(proyecto.Id))
                return BadRequest("Datos inválidos.");

            _service.AddProyecto(proyecto);
            return CreatedAtAction(nameof(GetProyecto), new { id = proyecto.Id }, proyecto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProyecto(string id, [FromBody] Proyecto proyecto)
        {
            if (id != proyecto.Id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo.");

            var existing = _service.GetProyectoById(id);
            if (existing == null)
                return NotFound($"Proyecto con ID {id} no encontrado.");

            _service.UpdateProyecto(proyecto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult HideProyecto(string id)
        {
            var proyecto = _service.GetProyectoById(id);
            if (proyecto == null)
                return NotFound($"Proyecto con ID {id} no encontrado.");

            _service.HideProyecto(id);
            return NoContent();
        }
    }
}
