using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFGGameServer.Core
{
    public class GameInfoBase
    {
        private string id;
        private int maxPlayers;
        public GameInfoBase(string id, int maxPlayers)
        {
            this.id = id;
            this.maxPlayers = maxPlayers;
        }
        public string Id { get { return id; } }
        public int MaxPlayers { get { return maxPlayers; } }
    }
}
