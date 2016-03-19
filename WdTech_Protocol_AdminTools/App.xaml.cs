using System.Net;
using System.Windows;
using WdTech_Protocol_AdminTools.Models;
using WdTech_Protocol_AdminTools.TcpCore;

namespace WdTech_Protocol_AdminTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            CommunicationServices.Start(new IPEndPoint(AppConfig.ServerIpAddress, AppConfig.ServerPort));

            base.OnStartup(e);
        }
    }
}
