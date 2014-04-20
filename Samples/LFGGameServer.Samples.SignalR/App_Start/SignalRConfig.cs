using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(LFGGameServer.Samples.SignalR.SignalRConfig))]
namespace LFGGameServer.Samples.SignalR
{
    public class SignalRConfig
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}