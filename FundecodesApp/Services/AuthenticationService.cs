using FundecodesApp.Entities;
using FundecodesApp.Services;

namespace FundecodesApp.Helpers
{
    public class AuthenticationService : IAuthService
    {
        private readonly UsuarioService _usuarioService;
        private readonly VoluntarioService _voluntarioService;
        private readonly AliadoService _aliadoService;

        public AuthenticationService(
            UsuarioService usuarioService,
            VoluntarioService voluntarioService,
            AliadoService aliadoService)
        {
            _usuarioService = usuarioService;
            _voluntarioService = voluntarioService;
            _aliadoService = aliadoService;
        }

        public Task<BaseUsuario?> AuthenticateAsync(string id, string password, string role)
        {
            // Buscar en usuarios
            var user = _usuarioService.GetUsuarios()
                .FirstOrDefault(u => u.Id == id && u.Role == role);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                return Task.FromResult<BaseUsuario?>(user);

            // Buscar en voluntarios
            var voluntario = _voluntarioService.GetVoluntarios()
                .FirstOrDefault(v => v.Id == id && v.Role == role);

            if (voluntario != null && BCrypt.Net.BCrypt.Verify(password, voluntario.Password))
                return Task.FromResult<BaseUsuario?>(voluntario);

            // Buscar en aliados
            var aliado = _aliadoService.GetAliados()
                .FirstOrDefault(a => a.Id == id && a.Role == role);

            if (aliado != null && BCrypt.Net.BCrypt.Verify(password, aliado.Password))
                return Task.FromResult<BaseUsuario?>(aliado);

            return Task.FromResult<BaseUsuario?>(null);
        }
    }
}
