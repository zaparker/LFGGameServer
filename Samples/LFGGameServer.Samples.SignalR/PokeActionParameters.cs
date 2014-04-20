using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFGGameServer.Samples.SignalR
{
    public class PokeActionParameters
    {
        public SamplePlayerInfo Poker { get; set; }
        public SamplePlayerInfo Pokee { get; set; }
    }
}