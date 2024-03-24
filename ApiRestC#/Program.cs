using ApiRestC_.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

TokenService.ConfigureJWTAuth(builder.Services);

var app = builder.Build();


Configure(app, builder.Environment);

app.Run();


void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseHttpsRedirection();

    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseRouting();
    app.UseMiddleware<JwtIPValidationMiddleware>();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}