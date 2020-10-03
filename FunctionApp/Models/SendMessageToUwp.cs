using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using FunctionApp;

namespace FunctionApp
{
    public static class SendMessageToUwp
    {
       
        private static readonly ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHubConnection"));
        [FunctionName("SendMessageToUwp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string targetdeviceId = req.Query["targetdeviceid"];
            string message = req.Query["message"];


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<BodyMessageModel>(requestBody);
            targetdeviceId = targetdeviceId ?? data?.TargetDeviceId;

            message = message ?? data?.Message;
            await DeviceService.SendMessageToDeviceAsync(serviceClient, targetdeviceId, message);

            return new OkResult();
        }
    }
}