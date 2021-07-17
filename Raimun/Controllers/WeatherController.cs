using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Raimun.App.Services.Implement.Weathers;
using Raimun.App.Services.Interface.Queues;
using Raimun.App.Services.Interface.Weathers;
using Raimun.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raimun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeatherController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IQueuesService _queuesService;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IWeathersService _weatherService;

        public WeatherController(IConfiguration config,
            IWeathersService weatherService,
            IQueuesService queuesService,
            IRecurringJobManager recurringJobManager
            )
        {
            _config = config;
            _queuesService = queuesService;
            _recurringJobManager = recurringJobManager;
            _weatherService = weatherService;
        }
        
        [HttpGet]
        public async Task<ActionResult<WeatherData>> Get([FromBody] WeatherCityRequest request)
        {
            var result = await _weatherService.GetWeatherDataByCity(request.CityName);
            
            _recurringJobManager.AddOrUpdate("GetWeather", 
                ()=> SetCityjob(request.CityName) , Cron.MinuteInterval(request.MinuteInterval));

            return Ok(result);
        }

        [Route("bylocation")]
        [HttpGet]
        public async Task<ActionResult<WeatherData>> GetByLocation([FromBody] WeatherLocationRequest request)
        {
            var result = await _weatherService.GetWeatherDataByLatLon(request.lat,request.lon);

            _recurringJobManager.AddOrUpdate("GetWeatherLocation",
                () => SetLocationjob( request.lat, request.lon), Cron.MinuteInterval(request.MinuteInterval));

            return Ok(result);
        }

        [NonAction]
        public async Task SetCityjob(string cityName)
        {
            var weatherResult = await _weatherService.GetWeatherDataByCity(cityName);
            _queuesService.PublishWeatherInfo(weatherResult);
        }

        [NonAction]
        public async Task SetLocationjob(int lat , int lon)
        {
            var weatherResult = await _weatherService.GetWeatherDataByLatLon(lat,lon);
            _queuesService.PublishWeatherInfo(weatherResult);
        }
    }
}
