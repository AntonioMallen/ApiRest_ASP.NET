using ProyectoJWT.Models;

namespace ProyectoJWT.Entities
{
    public class UsuarioEntity : IUsuarioEntity
    {
        private  List<Usuario> _usuarios;

        public UsuarioEntity() 
        {
            _usuarios = CrearUsuarios();

        }



        public Usuario GetUsuarioByID(int idUsuario)
        {
            return _usuarios.FirstOrDefault(usuario => usuario.id == idUsuario);
        }

        public List<Usuario> GetUsuarios()
        {
            return _usuarios;
        }

        public bool AddUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                _usuarios.Add(usuario);
                _usuarios = _usuarios.OrderBy(user => user.id).ToList();

                return true;
            }
            else 
            {
                return false;
            }
        }

        public bool DeleteUsuario(int id)
        {

            Usuario usuario = _usuarios.FirstOrDefault(usuario => usuario.id==id);
            if (usuario != null)
            {
                _usuarios.Remove(usuario);
                _usuarios  = _usuarios.OrderBy(user => user.id).ToList();
                return true;
            }
            else
            {
                return false;
            }
        }

       public bool UpdateUsuario(Usuario usuario)
       {
           if (usuario != null)
           {
              
               Usuario userLista = _usuarios.FirstOrDefault(usuarioLista => usuarioLista.id == usuario.id);
               if (userLista != null) 
               {
                   _usuarios.Remove(userLista);
                   _usuarios.Add(usuario);
                   _usuarios  = _usuarios.OrderBy(user => user.id).ToList();

                   return true;
               }
           }
           return false;
       }

        private List<Usuario> CrearUsuarios()
        {
            List<Usuario> personas = Enumerable.Range(1, 7).Select(indeusuario => new Usuario
            {
                id = indeusuario,
                name = "Persona " + indeusuario,
                email = "Persona " + indeusuario + "@gmail.es"
            }).ToList();

            return personas;
        }

    }
}
