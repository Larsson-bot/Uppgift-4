using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp
{
    public class DeviceService
    {
        public static async Task SendMessageToDeviceAsync(ServiceClient serviceClient, string targetDeviceId, string message)
        {
            var payload = new Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, payload);

        }
    }
}
