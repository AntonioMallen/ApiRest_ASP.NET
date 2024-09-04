using ProyectoJWT.Dtos;
using ProyectoJWT.Models;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoJWT.Mapper
{
    public class AutoMapperApp : Profile
    {
        public AutoMapperApp() 
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();

        }
    }
}
