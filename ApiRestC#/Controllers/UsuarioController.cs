using ProyectoJWT.Dtos;
using ProyectoJWT.Services;
using Microsoft.AspNetCore.Mvc;
using ProyectoJWT.Validator;
using Microsoft.AspNetCore.Identity;
using FluentValidation.Results;
namespace ProyectoJWT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuariosService _usuarioService;


        public UsuarioController(IUsuariosService usuarioService)
        {
            _usuarioService = usuarioService;
        }



        [HttpGet("GetUsuarios")]
        public IActionResult GetUsuarios()
        {
            return Ok(_usuarioService.GetUsuarios());
        }


        [HttpGet("GetUsuarioID/{id}")]
        public IActionResult GetUsuarioID(int id)
        {
            UsuarioDTO usuario = _usuarioService.GetUsuarioByID(id);

            var validation = new UsuarioValidator();
            validation.Validate(usuario);
            ValidationResult resultValidacion = validation.Validate(usuario);
            if (resultValidacion.Errors.Count > 0)
            {
                return BadRequest(resultValidacion.Errors[0]);
            }

            if (usuario != null)
            {
                return Ok(usuario);
            }
            else
            { 
                return NotFound();
            }
        }


        [HttpPost("UpdatePersonas")]
        public IActionResult UpdatePersonas(UsuarioDTO usuario)
        {
            var validation = new UsuarioValidator();
            validation.Validate(usuario);
            ValidationResult resultValidacion = validation.Validate(usuario);
            if (resultValidacion.Errors.Count > 0)
            {
                return BadRequest(resultValidacion.Errors[0]);
            }


            if (_usuarioService.UpdateUsuario(usuario))
            {
                return Ok(_usuarioService.GetUsuarios());
            }
            else
            { 
                return BadRequest();
            }

        }

        [HttpPut("AddPersonas")]
        public IActionResult AddPersonas(UsuarioDTO usuario)
        {
            var validation = new UsuarioValidator();
            validation.Validate(usuario);
            ValidationResult resultValidacion = validation.Validate(usuario);
            if (resultValidacion.Errors.Count > 0)
            {
                return BadRequest(resultValidacion.Errors[0]);
            }

            if (_usuarioService.AddUsuario(usuario))
            {
                return Ok(_usuarioService.GetUsuarios());
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete("DeletePersonas/{id}")]
        public IActionResult DeletePersonas(int id)
        {

            if (_usuarioService.DeleteUsuario(id))
            {
                return Ok(_usuarioService.GetUsuarios());
            }
            else
            {
                return BadRequest();
            }

        }


    }
}
