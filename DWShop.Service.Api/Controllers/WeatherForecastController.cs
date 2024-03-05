using Microsoft.AspNetCore.Mvc;

namespace DWShop.Service.Api.Controllers
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


        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
          )
        {
            _logger = logger;
          
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("GetSingletonGuid")]
        public string GetSingleton() {
            
            return singletonService.GetGuid();
        
        }



        [HttpGet("GetTransientGuid")]
        public string GetTransient()
        {
            
            string dobleGuid = $"{str2} {str1}";
            return dobleGuid;

        }

        [HttpGet("GetScopedGuid")]
        public string GetScoped()
        {
           
            string dobleGuid = $"{str2} {str1}";
            return dobleGuid;

        }
    }
}
