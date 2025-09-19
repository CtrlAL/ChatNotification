using Chat.Domain;
using Chat.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IRedisCache _cache;

        public WeatherController(IRedisCache cache)
        {
            _cache = cache;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var cachedWeather = await _cache.GetStringAsync(city);
            if (cachedWeather != null)
            {
                return Ok(new { Source = "Cache", Data = JsonSerializer.Deserialize<WeatherData>(cachedWeather) });
            }

            var weather = new WeatherData
            {
                City = city,
                Temperature = Random.Shared.Next(-20, 40),
                Condition = "Sunny"
            };

            await _cache.SetStringAsync(
                city,
                JsonSerializer.Serialize(weather), 
                TimeSpan.FromMinutes(5));

            return Ok(new { Source = "API", Data = weather });
        }
    }

    
}
