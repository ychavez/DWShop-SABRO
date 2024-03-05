using DWShop.Service.Api.Services;
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
        private readonly SingletonService singletonService;
        private readonly TransientService transientService;
        private readonly ScoopedService scoopedService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            SingletonService singletonService,
            TransientService transientService,
            ScoopedService scoopedService)
        {
            _logger = logger;
            this.singletonService = singletonService;
            this.transientService = transientService;
            this.scoopedService = scoopedService;
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
            string str1 = transientService.GetGuid();
            string str2 = transientService.GetGuid();
            string dobleGuid = $"{str2} {str1}";
            return dobleGuid;

        }

        [HttpGet("GetScopedGuid")]
        public string GetScoped()
        {
            string str1 = scoopedService.GetGuid();
            string str2 = scoopedService.GetGuid();
            string dobleGuid = $"{str2} {str1}";
            return dobleGuid;

        }
    }
}
