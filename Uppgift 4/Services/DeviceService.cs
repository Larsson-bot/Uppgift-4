using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Testing
{
    public static class DeviceService
    {
        private static readonly Random random = new Random();
     
        public static async Task SendMessageAsync(DeviceClient deviceClient)
        {

            var data = new TemperatureModels
            {
                Temperature = random.Next(20, 40),
                Humidity = random.Next(35, 50)

            };
            var json = JsonConvert.SerializeObject(data);

            var payload = new Message(Encoding.UTF8.GetBytes(json));
            await deviceClient.SendEventAsync(payload);
        }
    }
}
 