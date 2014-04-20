using LFGGameServer.Core;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFGGameServer.Samples.SignalR
{
    public class SignalRMessageSender : IMessageSender<SamplePlayerInfo, SampleGameInfo>
    {
        private IHubContext HubContext { get { return GlobalHost.ConnectionManager.GetHubContext<SampleHub>(); } }

        private dynamic GetGroup(string messageGroup, string gameId)
        {
            if (messageGroup == LFGGameServer.Core.MessageGroups.All) return HubContext.Clients.Group(gameId);
            else return HubContext.Clients.Group(gameId + "." + messageGroup);
        }

        public void SendPlayerJoinedMessage(string messageGroup, SamplePlayerInfo joinedPlayer, SampleGameInfo gameInfo)
        {
            GetGroup(messageGroup, gameInfo.Id).playerJoined(joinedPlayer);
        }

        public void SendPlayerLeftMessage(string messageGroup, SamplePlayerInfo joinedPlayer, SampleGameInfo gameInfo)
        {
            GetGroup(messageGroup, gameInfo.Id).playerleft(joinedPlayer);
        }

        public void SendGameEndMessage(string messageGroup, SampleGameInfo gameInfo)
        {
            GetGroup(messageGroup, gameInfo.Id).gameEnd();
        }

        public void SendActionMessage(string messageGroup, SampleGameInfo gameInfo, string action, object parameters)
        {
            var group = GetGroup(messageGroup, gameInfo.Id);
            switch(action)
            {
                case SampleActions.Poke:
                    HandlePokeAction(group, parameters as PokeActionParameters);
                    break;
                case SampleActions.Tick:
                    group.tick();
                    break;
            }
        }

        public void SendGameInfoMessage(string messageGroup, SampleGameInfo gameInfo)
        {
            GetGroup(messageGroup, gameInfo.Id).gameInfo(gameInfo);
        }

        public void SendPlayerInfoMessage(string messageGroup, SamplePlayerInfo playerInfo, SampleGameInfo gameInfo)
        {
            GetGroup(messageGroup, gameInfo.Id).playerInfo(playerInfo);
        }

        public void SendPlayerJoinedMessage(SamplePlayerInfo receiver, SamplePlayerInfo joinedPlayer, SampleGameInfo gameInfo)
        {
            HubContext.Clients.User(receiver.Id).playerJoined(joinedPlayer);
        }

        public void SendPlayerLeftMessage(SamplePlayerInfo receiver, SamplePlayerInfo joinedPlayer, SampleGameInfo gameInfo)
        {
            HubContext.Clients.User(receiver.Id).playerLeft(joinedPlayer);
        }

        public void SendGameEndMessage(SamplePlayerInfo receiver, SampleGameInfo gameInfo)
        {
            HubContext.Clients.User(receiver.Id).gameEnd();
        }

        public void SendActionMessage(SamplePlayerInfo receiver, SampleGameInfo gameInfo, string action, object parameters)
        {
            var user = HubContext.Clients.User(receiver.Id);
            switch(action)
            {
                case SampleActions.Poke:
                    HandlePokeAction(user, parameters as PokeActionParameters);
                    break;
            }
        }

        public void SendGameInfoMessage(SamplePlayerInfo receiver, SampleGameInfo gameInfo)
        {
            HubContext.Clients.User(receiver.Id).gameInfo(gameInfo);
        }

        public void SendPlayerInfoMessage(SamplePlayerInfo receiver, SamplePlayerInfo playerInfo, SampleGameInfo gameInfo)
        {
            HubContext.Clients.User(receiver.Id).playerInfo(playerInfo);
        }

        private void HandlePokeAction(dynamic receivers, PokeActionParameters parameters)
        {
            receivers.onPoke(parameters);
        }
    }
}