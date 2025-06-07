using FundecodesApp.DTO;
using FundecodesApp.Entities;
using FundecodesApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundecodesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Voluntario")]
    public class UserVoluntarioController : ControllerBase
    {
        private readonly UserVoluntarioService _service;

        public UserVoluntarioController(UserVoluntarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAllUserVoluntarios());

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var user = _service.GetUserVoluntarioById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserVoluntarioDto dto)
        {
            var newUser = new UserVoluntario
            {
                Id = Guid.NewGuid().ToString(), // Asegúrate de generar un ID
                Name = dto.Name,
                Email = dto.Email,
                Number = dto.Number
            };

            _service.AddUserVoluntario(newUser);
            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] UserVoluntarioDto dto)
        {
            var existing = _service.GetUserVoluntarioById(id);
            if (existing == null) return NotFound();

            var updated = new UserVoluntario
            {
                Id = id,
                Name = dto.Name,
                Email = dto.Email,
                Number = dto.Number
            };

            _service.UpdateUserVoluntario(id, updated);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var existing = _service.GetUserVoluntarioById(id);
            if (existing == null) return NotFound();

            _service.DeleteUserVoluntario(id);
            return Ok("Eliminado correctamente");
        }
    }
}
