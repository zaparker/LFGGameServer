using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFGGameServer.Core
{
    public interface IGameInstanceGenerator<TPlayer, TGame>
        where TPlayer : PlayerInfoBase
        where TGame : GameInfoBase
    {
        GameInstanceBase<TPlayer, TGame> CreateGameInstance(TGame gameInfo);
    }
}
