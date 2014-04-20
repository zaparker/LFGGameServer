using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFGGameServer.Core
{
    public interface ILobbyMessageSender<TPlayer, TGame>
        where TPlayer : PlayerInfoBase
        where TGame : GameInfoBase
    {
        void SendGameCreatedMessage(TGame gameInfo);
        void SendGameEndedMessage(TGame gameInfo);
    }
}
