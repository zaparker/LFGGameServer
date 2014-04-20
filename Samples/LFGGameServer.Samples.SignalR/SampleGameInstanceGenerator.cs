using LFGGameServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFGGameServer.Samples.SignalR
{
    public class SampleGameInstanceGenerator : IGameInstanceGenerator<SamplePlayerInfo, SampleGameInfo>
    {
        private SignalRMessageSender messageSender = new SignalRMessageSender();

        public GameInstanceBase<SamplePlayerInfo, SampleGameInfo> CreateGameInstance(SampleGameInfo gameInfo)
        {
            return new SampleGameInstance(gameInfo, messageSender);
        }
    }
}