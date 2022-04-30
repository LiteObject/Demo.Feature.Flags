using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace Demo.Feature.Flags.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IFeatureManager _featureManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IFeatureManager featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            if (await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureB))
            {
                // Run the following code
                _logger.LogInformation($"{nameof(MyFeatureFlags.FeatureB)} is enabled.");
            }

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [FeatureGate(MyFeatureFlags.FeatureA)]
        [HttpGet("hello")]        
        public async Task<IActionResult> Hello()
        {
            var result = await Task.FromResult("Hello");
            return Ok(result);
        }
    }
}