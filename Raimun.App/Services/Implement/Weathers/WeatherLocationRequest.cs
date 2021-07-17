using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raimun.App.Services.Implement.Weathers
{
    public class WeatherLocationRequest
    {
        [Required]
        public int lat { get; set; }
        
        [Required]
        public int lon { get; set; }
        
        [Required]
        [Range(1,60)]
        public int MinuteInterval { get; set; }
    }
}
