using Raimun.App.Services.Implement.Weathers;
using Raimun.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raimun.App.Services.Interface.Queues
{
    public interface IQueuesService
    {
        void PublishWeatherInfo(WeatherData data);
    }
}
