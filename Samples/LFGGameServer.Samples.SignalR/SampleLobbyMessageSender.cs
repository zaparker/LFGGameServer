using LFGGameServer.Core;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFGGameServer.Samples.SignalR
{
    public class SampleLobbyMessageSender : ILobbyMessageSender<SamplePlayerInfo, SampleGameInfo>
    {
        private IHubContext HubContext { get { return GlobalHost.ConnectionManager.GetHubContext<SampleHub>(); } }

        public void SendGameCreatedMessage(SampleGameInfo gameInfo)
        {
            HubContext.Clients.Group(MessageGroups.Lobby).addGameToLobby(gameInfo);
        }

        public void SendGameEndedMessage(SampleGameInfo gameInfo)
        {
            HubContext.Clients.Group(MessageGroups.Lobby).removeGameFromLobby(gameInfo);
        }
    }
}