using MassTransit;
using Raimun.App.Services.Implement.Weathers;
using Raimun.App.Services.Interface.Queues;
using Raimun.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raimun.App.Services.Implement.Queues
{
    public class QueuesService : IQueuesService
    {
        private readonly IBus _bus;

        public QueuesService(IBus bus)
        {
            _bus = bus;
        }

        public async void PublishWeatherInfo(WeatherData data)
        {
            if (data != null)
            {
                //ticket.BookedOn = DateTime.Now;
                
                Uri uri = new Uri("rabbitmq://rabbitmq/weatherqueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(data);
                await _bus.Publish(data);
            }
        }
    }
}
