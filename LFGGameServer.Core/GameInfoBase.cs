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
        public GameInfoBase(string id)
        {
            this.id = id;
        }
        public string Id { get { return id; } }
    }
}
