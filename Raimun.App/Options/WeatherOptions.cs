using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raimun.App.Options
{
    public class WeatherOptions
    {
        public string BaseUrl { get; set; }
        public string ApiCityUri { get; set; }
        public string ApiLocationUri { get; set; }
        public string ApiKey { get; set; }
    }
}
