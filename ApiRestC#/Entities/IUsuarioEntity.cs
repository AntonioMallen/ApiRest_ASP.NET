using ProyectoJWT.Models;

namespace ProyectoJWT.Entities
{
    public interface IUsuarioEntity
    {
        public List<Usuario> GetUsuarios();
        public Usuario GetUsuarioByID(int idUsuario);
        public bool AddUsuario(Usuario usuario);
        public bool UpdateUsuario(Usuario usuario);
        public bool DeleteUsuario(int id);
    }
}
