using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raimun.App.Services.Implement.Weathers
{
    public class WeatherCityRequest
    {
        [Required]
        public string CityName { get; set; }
        [Required]
        [Range(1, 60)]
        public int MinuteInterval { get; set; }
    }
}
