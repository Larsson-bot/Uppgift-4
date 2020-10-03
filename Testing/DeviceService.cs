using MAD = Microsoft.Azure.Devices;
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
        public static async Task<string> ReceiveMessageAsync(DeviceClient deviceClient)
        { 
            
            while (true)
            {
                Message payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                    continue;
             
                   Encoding.UTF8.GetString(payload.GetBytes());
                
                await deviceClient.CompleteAsync(payload);
                 
                
                
                
            }


        }
        public static async Task SendMessageToDeviceAsync(MAD.ServiceClient serviceClient, string targetDeviceId, string message)
        {
            var payload = new MAD.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, payload);

        }
    }
}
