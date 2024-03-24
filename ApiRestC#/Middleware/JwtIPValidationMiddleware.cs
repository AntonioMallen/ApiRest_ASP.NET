using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

public class JwtIPValidationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtIPValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {

        ComprobarIp(context);

        await _next(context);
    }

    /// <summary>
    /// Metodo  para verificar que la IP del token es la misma que la de la llamada
    /// </summary>
    /// <param name="context">Contexto de la applicacion</param>
    private static async void ComprobarIp(HttpContext context) 
    {
        var token = await context.GetTokenAsync("access_token");

        if (!string.IsNullOrEmpty(token))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var ipClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "IP")?.Value;

            if (!string.IsNullOrEmpty(ipClaim))
            {
                var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString();

                if (ipClaim != remoteIpAddress)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
            }
        }
    }
}