using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Testing;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using System.Text;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Uppgift_4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly string _conn = "HostName=Uppgift3.azure-devices.net;DeviceId=Uppgift4;SharedAccessKey=7k3jGCYVVJGgRxzHij+TxCyZUz5bPzr/y8BeWRGHca4=";
        public static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(_conn, TransportType.Mqtt);
      
        public MainPage()
        {
            this.InitializeComponent();

            ReceiveMessageAsync().GetAwaiter();
          
                
            
            

        }

        private  void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            DeviceService.SendMessageAsync(deviceClient).GetAwaiter();

        }

        private async Task ReceiveMessageAsync()
        {
            while (true)
            {
                Message payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                    continue;

              lvMessages.Items.Add(Encoding.UTF8.GetString(payload.GetBytes()));

                await deviceClient.CompleteAsync(payload);
            }
        }
    }
      
   
}

