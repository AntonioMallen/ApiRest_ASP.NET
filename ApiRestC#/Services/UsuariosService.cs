using ProyectoJWT.Dtos;
using ProyectoJWT.Entities;
using ProyectoJWT.Mapper;
using ProyectoJWT.Models;
using AutoMapper;

namespace ProyectoJWT.Services
{
    public class UsuariosService : IUsuariosService
    {

        private readonly IUsuarioEntity _usuarioEntity;
        private readonly IMapper _mapper;

        public UsuariosService(IUsuarioEntity usuarioEntity, IMapper mapper) 
        {
            _usuarioEntity=usuarioEntity;
            _mapper = mapper;
        }


        public bool AddUsuario(UsuarioDTO usuario)
        {
            Usuario usuarioMapeado = _mapper.Map<Usuario>(usuario);

            return _usuarioEntity.AddUsuario(usuarioMapeado);
        }

        public bool DeleteUsuario(int id)
        {
            return _usuarioEntity.DeleteUsuario(id);
        }

        public UsuarioDTO GetUsuarioByID(int idUsuario)
        {
            Usuario user = _usuarioEntity.GetUsuarioByID(idUsuario);
            UsuarioDTO usuarioMapeado = _mapper.Map<UsuarioDTO>(user);
            return usuarioMapeado;
        }

        public List<UsuarioDTO> GetUsuarios()
        {
            List<Usuario> list = _usuarioEntity.GetUsuarios();
            List<UsuarioDTO> listDTO = _mapper.Map<List<UsuarioDTO>>(list);
            return listDTO;
        }

        public bool UpdateUsuario(UsuarioDTO usuario)
        {
            Usuario usuarioMapeado = _mapper.Map<Usuario>(usuario);

            return _usuarioEntity.UpdateUsuario(usuarioMapeado);
        }
    }
}
