using ApiRestC_.Clases;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace ApiRestC_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        
        
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        private IEnumerable<WeatherForecast> Tiempo = Enumerable.Range(1, 7).Select(index => new WeatherForecast
        {
            id = index,
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();


        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> GetWeather()
        {
            return Tiempo;
        }
        [HttpGet("GetPorID/{id}")]
        public WeatherForecast GetID(int id)
        {
            return Tiempo.FirstOrDefault(x => x.id == id);
        }
    }
}
