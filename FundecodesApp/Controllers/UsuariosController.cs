using Microsoft.AspNetCore.Mvc;
using FundecodesApp.Entities;
using FundecodesApp.Services;

namespace FundecodesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuariosController()
        {
            _service = new UsuarioService();
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var usuarios = _service.GetUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuario(string id)
        {
            var usuario = _service.GetUsuarioById(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult AddUsuario([FromBody] Usuario usuario)
        {
            _service.AddUsuario(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(string id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id) return BadRequest();
            _service.UpdateUsuario(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult HideUsuario(string id)
        {
            _service.HideUsuario(id);
            return NoContent();
        }
    }
}
