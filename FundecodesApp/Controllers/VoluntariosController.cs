using Microsoft.AspNetCore.Mvc;
using FundecodesApp.Entities;
using FundecodesApp.Services;

namespace FundecodesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoluntariosController : ControllerBase
    {
        private readonly VoluntarioService _service;

        public VoluntariosController()
        {
            _service = new VoluntarioService();
        }

        [HttpGet]
        public IActionResult GetVoluntarios()
        {
            var voluntarios = _service.GetVoluntarios();
            return Ok(voluntarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetVoluntario(string id)
        {
            var voluntario = _service.GetVoluntarioById(id);
            if (voluntario == null) return NotFound();
            return Ok(voluntario);
        }

        [HttpPost]
        public IActionResult AddVoluntario([FromBody] Voluntario voluntario)
        {
            _service.AddVoluntario(voluntario);
            return CreatedAtAction(nameof(GetVoluntario), new { id = voluntario.Id }, voluntario);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVoluntario(string id, [FromBody] Voluntario voluntario)
        {
            if (id != voluntario.Id) return BadRequest();
            _service.UpdateVoluntario(voluntario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult HideVoluntario(string id)
        {
            _service.HideVoluntario(id);
            return NoContent();
        }
    }
}

