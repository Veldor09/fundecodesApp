using FundecodesApp.DTO;
using FundecodesApp.Entities;
using FundecodesApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundecodesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Aliado")]
    public class UserAliadoController : ControllerBase
    {
        private readonly UserAliadoService _service;

        public UserAliadoController(UserAliadoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAllUserAliados());

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var user = _service.GetUserAliadoById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserAliadoDto dto)
        {
            var newUser = new UserAliado
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Email = dto.Email,
                Number = dto.Number
            };

            _service.AddUserAliado(newUser);
            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] UserAliadoDto dto)
        {
            var existing = _service.GetUserAliadoById(id);
            if (existing == null) return NotFound();

            var updated = new UserAliado
            {
                Id = id,
                Name = dto.Name,
                Email = dto.Email,
                Number = dto.Number
            };

            _service.UpdateUserAliado(id, updated);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var existing = _service.GetUserAliadoById(id);
            if (existing == null) return NotFound();

            _service.DeleteUserAliado(id);
            return Ok("Eliminado correctamente");
        }
    }
}
