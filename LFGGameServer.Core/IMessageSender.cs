using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFGGameServer.Core
{
    public interface IMessageSender<TPlayer, TGame>
        where TPlayer : PlayerInfoBase
        where TGame : GameInfoBase
    {
        void SendPlayerJoinedMessage(string messageGroup, TPlayer joinedPlayer, TGame gameInfo);
        void SendPlayerLeftMessage(string messageGroup, TPlayer joinedPlayer, TGame gameInfo);
        void SendGameEndMessage(string messageGroup, TGame gameInfo);
        void SendActionMessage(string messageGroup, TGame gameInfo, string action, object parameters);
        void SendGameInfoMessage(string messageGroup, TGame gameInfo);
        void SendPlayerInfoMessage(string messageGroup, TPlayer playerInfo, TGame gameInfo);

        void SendPlayerJoinedMessage(TPlayer receiver, TPlayer joinedPlayer, TGame gameInfo);
        void SendPlayerLeftMessage(TPlayer receiver, TPlayer joinedPlayer, TGame gameInfo);
        void SendGameEndMessage(TPlayer receiver, TGame gameInfo);
        void SendActionMessage(TPlayer receiver, TGame gameInfo, string action, object parameters);
        void SendGameInfoMessage(TPlayer receiver, TGame gameInfo);
        void SendPlayerInfoMessage(TPlayer receiver, TPlayer playerInfo, TGame gameInfo);
    }
}
