using MassTransit;
using Raimun.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raimun.consoleapp
{
    class WeatherDataConsumer : IConsumer<WeatherData>
    {
        public async Task Consume(ConsumeContext<WeatherData> context)
        {
            Console.WriteLine( string.Format("{0}:{1} - temp: {2} c" , DateTime.Now.ToShortTimeString(),
                context.Message.Title , context.Message.Main.Temperature));
        }
    }
    
}
