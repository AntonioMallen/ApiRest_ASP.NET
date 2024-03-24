using ApiRestC_.Clases;
using ApiRestC_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiRestC_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {

        private readonly ILogger<PersonasController> _logger;

        private List<Usuario> Personas;

        public PersonasController(ILogger<PersonasController> logger)
        {
            _logger = logger;

            Personas = CrearPersonas();

        }


        private List<Usuario> CrearPersonas()
        {
            List<Usuario> personas = Enumerable.Range(1, 7).Select(index => new Usuario
            {
                id = index,
                name = "Persona " + index,
                correo = "Persona " + index + "@gmail.es"
            }).ToList();

            return personas;
        }


        [HttpGet("GetPersonas"), Authorize]
        public IActionResult GetPersonas()
        {
            return Ok(Personas);
        }


        [HttpGet("GetPorID/{id}"), Authorize]
        public IActionResult GetID(int id)
        {
            Usuario persona = Personas.FirstOrDefault(x => x.id == id);
            if (persona == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(persona);
            }
        }


        [HttpPost("PostPersonas"), Authorize]
        public IActionResult PostPersonas(Usuario persona)
        {
            Personas.Add(persona);

            return Ok(Personas);
        }


        [HttpDelete("DeletePersonas/{id}"), Authorize]
        public IActionResult DeletePersonas(int id)
        {
            Usuario personaEliminar = Personas.FirstOrDefault(x => x.id == id);

            if (Personas.Remove(personaEliminar))
            {
                return Ok(Personas);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("GetToken")]
        [AllowAnonymous]
        public IActionResult GetToken()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress;
            string ipAddressString = ipAddress.ToString();
            // Verificar si el contexto HTTP actual no es nulo


            Claim[] claims;
            Claim nombreClaim = new Claim("nombre", "Antonio");
            Claim rolClaim = new Claim("rol", "Prueba");
            Claim ipClaim = new Claim("IP", ipAddressString);

            string token = TokenService.GenerateToken([nombreClaim, rolClaim, ipClaim]);

            return Ok(token);


        }


        //[HttpGet("UseToken/{token}"), Authorize]
        //public IActionResult UseToken(string token)
        //{
        //    IEnumerable<Claim> claims = TokenService.ValidateToken(token);

        //    Claim claimUser = null;

        //    if (claims != null) { claimUser = claims.FirstOrDefault(x => x.Type == "nombre"); }

        //    if (claimUser == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok("Usuario: " + claimUser.Value);
        //    }
        //}
    }
}
