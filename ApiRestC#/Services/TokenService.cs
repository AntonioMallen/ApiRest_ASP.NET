using ProyectoJWT.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ProyectoJWT.Services
{
    public class TokenService : ITokenService
    {

    private readonly IConfiguration _config;
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

        public TokenService(IConfiguration configuration)
        {
            _config = configuration;
           IConfigurationSection seccion = _config.GetSection("JwtSettings");
           _secretKey = seccion.GetValue("SecretKey", "") ?? "";
            _issuer = seccion.GetValue("ValidIssuer", "") ?? "";
            _audience = seccion.GetValue("ValidAudience", "") ?? "";
        }



        /// <summary>
        /// Genera un token de acceso JWT
        /// </summary>
        /// <param name="claims">Informacion pasada a traves del Token</param>
        /// <returns></returns>
        public string GenerateToken(UsuarioDTO usuario, string ip)
        {
            string secretKey = _secretKey;
            string issuer = _issuer;
            string auddience = _audience;

            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            Claim[] claims = {
                new Claim("id", usuario.id+""),
                new Claim("nombre", usuario.name),
                new Claim("pass", usuario.pass),
                new Claim("IP", ip)
            };


            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: auddience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidarIP(string token, string ip)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var ipClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "IP")?.Value;

            if (!string.IsNullOrEmpty(ipClaim))
            {

                if (ipClaim == ip)
                {

                    return true;
                }
            }
            return false;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public static IEnumerable<Claim> ValidateToken(string token)
        //{

        //    var builder = new ConfigurationBuilder()
        //      .AddJsonFile("app.json", optional: false, reloadOnChange: true);

        //    IConfiguration configuration = builder.Build();

        //    ClaimsPrincipal principal = null;
        //    string secretKey = configuration["secretKey"];

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = new SymmetricSecurityKey(System.Teusuariot.Encoding.UTF32.GetBytes(secretKey));

        //    var validationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = key,
        //        ValidateIssuer = false, // Si tu token tiene un "issuer" y deseas validarlo, cámbialo a true
        //        ValidateAudience = false, // Si tu token tiene una "audience" y deseas validarlo, cámbialo a true
        //        ClockSkew = TimeSpan.Zero // No hay desfase en el tiempo
        //    };

        //    try
        //    {
        //        SecurityToken validatedToken;
        //        principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
        //        return principal.Claims.ToList();
        //    }
        //    catch (Eusuarioception eusuario)
        //    {
        //        // El token no es válido
        //        Console.WriteLine($"Error al validar el token: {eusuario.Message}");
        //        return null;
        //    }
        //}




    }
}
