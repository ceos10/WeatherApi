using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Controllers;

[ApiController]
//[Route("[controller]")]
[ApiVersion("1.0")]
[ApiVersion("1.1", Deprecated = true)]
[ApiVersion("2.0")]
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

    [Route("~/"), HttpGet]
    //[HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [Route("~/version-test/"), HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult Get20()
    {
        return Ok("From version 2.0");
    }
}

