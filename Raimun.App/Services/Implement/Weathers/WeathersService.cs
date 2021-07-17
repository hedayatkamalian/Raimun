using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Raimun.App.Options;
using Raimun.App.Services.Interface.Auth;
using Raimun.App.Services.Interface.Weathers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FluentScheduler;
using Raimun.Share;
using Raimun.App.Entities.Data;

namespace Raimun.App.Services.Implement.Weathers
{
    public class WeathersService  : IWeathersService
    {
        private readonly WeatherOptions _weatherOptions;
        private readonly ApplicationDbContext _context;

        public WeathersService(IOptions<WeatherOptions> weatherOptions ,
            ApplicationDbContext context)
        {
            _weatherOptions = weatherOptions.Value;
            _context = context;
        }

        public async Task<WeatherData> GetWeatherDataByCity(string cityName)
        {
            var client = new HttpClient();

            var url = _weatherOptions.BaseUrl + _weatherOptions.ApiCityUri
                .Replace("{cityName}", cityName)
                .Replace("{APIKey}", _weatherOptions.ApiKey);

            return await GetWeatherData(url);
        }

        public async Task<WeatherData> GetWeatherDataByLatLon(int lat , int lon)
        {
            var url = _weatherOptions.BaseUrl + _weatherOptions.ApiLocationUri
                .Replace("{lat}", lat.ToString())
                .Replace("{lon}", lon.ToString())
                .Replace("{APIKey}", _weatherOptions.ApiKey);

            return await GetWeatherData(url);
        }

        public async Task<WeatherData> GetWeatherData(string url)
        {
            var client = new HttpClient();

            WeatherData weatherData = null;
            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }

                if (weatherData != null && weatherData.Main.Temperature > 14)
                {
                    var generator = new IdGen.IdGenerator(0);
                    await _context.TempDatas.AddAsync(new Entities.TempData
                    {
                        Id = generator.CreateId(),
                        Name = weatherData.Title,
                        Temprature = weatherData.Main.Temperature
                    });

                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return weatherData;
        }

      
    }
}
