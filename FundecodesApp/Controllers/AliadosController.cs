using Microsoft.AspNetCore.Mvc;
using FundecodesApp.Entities;
using FundecodesApp.Services;

namespace FundecodesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AliadosController : ControllerBase
    {
        private readonly AliadoService _service;

        public AliadosController()
        {
            _service = new AliadoService();
        }

        [HttpGet]
        public IActionResult GetAliados()
        {
            var aliados = _service.GetAliados();
            return Ok(aliados);
        }

        [HttpGet("{id}")]
        public IActionResult GetAliado(string id)
        {
            var aliado = _service.GetAliadoById(id);
            if (aliado == null) return NotFound();
            return Ok(aliado);
        }

        [HttpPost]
        public IActionResult AddAliado([FromBody] Aliado aliado)
        {
            _service.AddAliado(aliado);
            return CreatedAtAction(nameof(GetAliado), new { id = aliado.Id }, aliado);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAliado(string id, [FromBody] Aliado aliado)
        {
            if (id != aliado.Id) return BadRequest();
            _service.UpdateAliado(aliado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult HideAliado(string id)
        {
            _service.HideAliado(id);
            return NoContent();
        }

    }
}

