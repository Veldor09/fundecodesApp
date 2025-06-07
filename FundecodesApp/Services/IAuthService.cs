using FundecodesApp.Entities;

public interface IAuthService
{
    Task<BaseUsuario?> AuthenticateAsync(string id, string password, string role);
}

