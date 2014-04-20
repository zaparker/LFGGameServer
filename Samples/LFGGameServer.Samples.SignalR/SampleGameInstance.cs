using LFGGameServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFGGameServer.Samples.SignalR
{
    public class SampleGameInstance : GameInstanceBase<SamplePlayerInfo, SampleGameInfo>
    {
        public SampleGameInstance(SampleGameInfo gameInfo, SignalRMessageSender messageSender)
            : base(gameInfo, messageSender)
        { }

        public void Poke(string poker, string pokee)
        {
            SendActionMessage(MessageGroups.All, SampleActions.Poke, new PokeActionParameters { Poker = GetPlayer(poker), Pokee = GetPlayer(pokee) });
        }
    }
}