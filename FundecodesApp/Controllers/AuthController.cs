
using FundecodesApp.DTO;
using FundecodesApp.Entities;
using FundecodesApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FundecodesApp.Helpers;
using System;

namespace FundecodesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        private readonly JwtSettings _jwtSettings;
        private readonly UsuarioService _usuarioService;
        private readonly VoluntarioService _voluntarioService;
        private readonly AliadoService _aliadoService;

        public AuthController(
            IAuthService authenticationService,
            JwtSettings jwtSettings,
            UsuarioService usuarioService,
            VoluntarioService voluntarioService,
            AliadoService aliadoService)
        {
            _authenticationService = authenticationService;
            _jwtSettings = jwtSettings;
            _usuarioService = usuarioService;
            _voluntarioService = voluntarioService;
            _aliadoService = aliadoService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterDto newUser)
        {
            // Validaciones básicas
            if (newUser == null)
                return BadRequest("Datos de usuario inválidos.");

            if (string.IsNullOrWhiteSpace(newUser.Role))
                return BadRequest("El campo 'Rol' es obligatorio.");

            var rol = newUser.Role.ToLower().Trim();

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            try
            {
                switch (rol)
                {
                    case "administrador":
                        _usuarioService.AddUsuario(new Usuario
                        {
                            Id = newUser.Id,
                            Name = newUser.Name,
                            Email = newUser.Email,
                            Password = hashedPassword,
                            Role = "Administrador"
                        });
                        break;

                    case "voluntario":
                        _voluntarioService.AddVoluntario(new Voluntario
                        {
                            Id = newUser.Id,
                            Name = newUser.Name,
                            Email = newUser.Email,
                            Password = hashedPassword,
                            Role = "Voluntario"
                        });
                        break;

                    case "aliado":
                        _aliadoService.AddAliado(new Aliado
                        {
                            Id = newUser.Id,
                            Name = newUser.Name,
                            Email = newUser.Email,
                            Password = hashedPassword,
                            Role = "Aliado"
                        });
                        break;

                    default:
                        return BadRequest("Rol inválido. Usa 'Administrador', 'Voluntario' o 'Aliado'.");
                }

                return Ok("Usuario registrado con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest("Datos de login inválidos.");

            var user = await _authenticationService.AuthenticateAsync(loginDto.Id, loginDto.Password, loginDto.Role);

            if (user == null)
                return Unauthorized("ID o contraseña incorrectos.");

            var token = TokenGenerator.GenerateToken(user, _jwtSettings);

            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Role
                }
            });
        }
    }
}
