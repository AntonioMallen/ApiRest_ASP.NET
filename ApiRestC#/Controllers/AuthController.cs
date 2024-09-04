using ProyectoJWT.Dtos;
using ProyectoJWT.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoJWT.Validator;
using FluentValidation.Results;
namespace ProyectoJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ITokenService tokenService) : ControllerBase
    {
        readonly ITokenService _tokenService = tokenService;



        [HttpPost("/auth")]
        [AllowAnonymous]
        public IActionResult GetToken([FromBody] UsuarioDTO usuario)
        {

            var validation = new UsuarioValidator();
            ValidationResult resultValidacion =validation.Validate(usuario);
            if (resultValidacion.Errors.Count>0)
            {
                return BadRequest(resultValidacion.Errors[0]);
            }

            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";

            string token = _tokenService.GenerateToken(usuario, ip);

            // Devolver el token generado
            return Ok(token);
        }
    }
}
