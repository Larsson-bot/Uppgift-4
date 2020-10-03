using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public static class DeviceService
    {

    


        public static async Task SendMessageToDeviceAsync(ServiceClient serviceClient, string targetDeviceId, string message)
        {
            var payload = new Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, payload);

        }
    }
}
