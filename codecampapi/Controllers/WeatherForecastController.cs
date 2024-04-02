using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace codecampapi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IDistributedCache _distributedCache;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IDistributedCache distributedCache)
    {
        _logger = logger;
        _distributedCache = distributedCache;
    }

    //[ResponseCache(Duration = 60)] 
    [HttpGet("GetWeatherForecast", Name = "GetWeatherForecast")]
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


    [HttpGet("GetCachedWeatherForecast")]
    public IEnumerable<WeatherForecast> GetCachedValue()
    {
        var cacheKey = "WeatherForecastCacheKey";

        // Attempt to retrieve cached data
        var cachedData = _distributedCache.GetString(cacheKey);
        
        if (cachedData != null)
        {
            _logger.LogInformation("From Cache");
            // Data found in cache, deserialize and return
            return JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(cachedData);
        }
        else
        {
            _logger.LogInformation("From Method");
            // Data not in cache, fetch from database or source
            var newWeatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            // Serialize and store data in cache for future use
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
            };

            _distributedCache.SetString(cacheKey, JsonSerializer.Serialize(newWeatherForecasts), cacheOptions);

            return newWeatherForecasts;
        }
    }
}
