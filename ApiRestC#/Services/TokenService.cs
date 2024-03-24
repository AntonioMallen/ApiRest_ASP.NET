using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
namespace ApiRestC_.Services
{
    public class TokenService
    {

        private readonly SymmetricSecurityKey _ssKey;

        public TokenService(IConfiguration config)
        {
            _ssKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(config["secretKey"]));
        }

        /// <summary>
        /// Configura la autenticación JWT y la documentación Swagger para la aplicación.
        /// </summary>
        /// <param name="services">La colección de servicios de la aplicación.</param>
        public static void ConfigureJWTAuth(IServiceCollection services)
        {
            var config = TokenService.ConstruirConfiguracion();
            string secretKey = config["secretKey"];
            string issuer = config["issuer"];
            string audience = config["audience"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(secretKey))
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                };
                c.AddSecurityRequirement(securityRequirement);
            });

            services.AddControllers();
        }


        /// <summary>
        /// Genera un token de acceso JWT
        /// </summary>
        /// <param name="claims">Informacion pasada a traves del Token</param>
        /// <returns></returns>
        public static string GenerateToken(params Claim[] claims)
        {
            IConfiguration config = ConstruirConfiguracion();

            string secretKey = config["secretKey"];
            string issuer = config["issuer"];
            string audience = config["audience"];


            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> ValidateToken(string token)
        {

            var builder = new ConfigurationBuilder()
              .AddJsonFile("app.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            ClaimsPrincipal principal = null;
            string secretKey = configuration["secretKey"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF32.GetBytes(secretKey));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false, // Si tu token tiene un "issuer" y deseas validarlo, cámbialo a true
                ValidateAudience = false, // Si tu token tiene una "audience" y deseas validarlo, cámbialo a true
                ClockSkew = TimeSpan.Zero // No hay desfase en el tiempo
            };

            try
            {
                SecurityToken validatedToken;
                principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                return principal.Claims.ToList();
            }
            catch (Exception ex)
            {
                // El token no es válido
                Console.WriteLine($"Error al validar el token: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene la configuracion de un archivo JSON
        /// </summary>
        /// <returns>La configuracion</returns>
        public static IConfiguration ConstruirConfiguracion()
        {
            var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();
            return configuration;
        }


    }
}
