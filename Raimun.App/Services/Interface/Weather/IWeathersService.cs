using Raimun.App.Services.Implement.Weathers;
using Raimun.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raimun.App.Services.Interface.Weathers
{
    public interface IWeathersService
    {
        Task<WeatherData> GetWeatherDataByCity(string cityName);
        Task<WeatherData> GetWeatherDataByLatLon(int lat , int lon);
    }
}
