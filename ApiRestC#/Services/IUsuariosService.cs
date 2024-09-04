using ProyectoJWT.Dtos;
using ProyectoJWT.Models;

namespace ProyectoJWT.Services
{
    public interface IUsuariosService
    {
        public List<UsuarioDTO> GetUsuarios();
        public UsuarioDTO GetUsuarioByID(int idUsuario);
        public bool AddUsuario(UsuarioDTO usuario);
        public bool UpdateUsuario(UsuarioDTO usuario);
        public bool DeleteUsuario(int id);
    }
}
