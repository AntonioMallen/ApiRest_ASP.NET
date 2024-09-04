using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using ProyectoJWT.Services;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _neusuariot;

    public TokenValidationMiddleware(RequestDelegate neusuariot)
    {
        _neusuariot = neusuariot;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        string ip = context.Connection.RemoteIpAddress?.ToString() ?? "";

        // Si se usa el token desde otra ip o no tiene token (Solo en auth), entra, sino 401
        if (!string.IsNullOrEmpty(token))
        {
            var tokenService = context.RequestServices.GetRequiredService<ITokenService>();

            if (tokenService.ValidarIP(token,ip))
            {
                await _neusuariot(context);
                return;
            }
        }
        else 
        {
            if (context.Request.Path.StartsWithSegments("/auth"))
            {
                await _neusuariot(context);
                return;
            }
        }
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Token no válido");

    }



}