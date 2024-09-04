using ProyectoJWT.Dtos;

namespace ProyectoJWT.Services
{
    public interface ITokenService
    {
        public string GenerateToken( UsuarioDTO usuario,string ipAddress);

        public bool ValidarIP(string token,string ip);

    }
}
