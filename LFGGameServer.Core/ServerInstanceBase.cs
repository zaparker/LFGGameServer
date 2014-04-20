using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFGGameServer.Core
{
    public class ServerInstanceBase<TPlayer, TGame>
        where TPlayer : PlayerInfoBase
        where TGame : GameInfoBase
    {
        private List<GameInstanceBase<TPlayer, TGame>> gameInstances = new List<GameInstanceBase<TPlayer, TGame>>();
        private List<TPlayer> players = new List<TPlayer>();
        private IGameInstanceGenerator<TPlayer, TGame> gameInstanceGenerator;
        private ILobbyMessageSender<TPlayer, TGame> lobbyMessageSender;
        private int maxGames = 1;

        public ServerInstanceBase(IGameInstanceGenerator<TPlayer, TGame> gameInstanceGenerator)
        {
            this.gameInstanceGenerator = gameInstanceGenerator;
        }

        public ServerInstanceBase(IGameInstanceGenerator<TPlayer, TGame> gameInstanceGenerator, int maxGames)
        {
            this.gameInstanceGenerator = gameInstanceGenerator;
            this.maxGames = maxGames;
        }

        public ServerInstanceBase(IGameInstanceGenerator<TPlayer, TGame> gameInstanceGenerator, ILobbyMessageSender<TPlayer, TGame> lobbyMessageSender)
        {
            this.gameInstanceGenerator = gameInstanceGenerator;
            this.lobbyMessageSender = lobbyMessageSender;
        }

        public ServerInstanceBase(IGameInstanceGenerator<TPlayer, TGame> gameInstanceGenerator, ILobbyMessageSender<TPlayer, TGame> lobbyMessageSender, int maxGames)
        {
            this.gameInstanceGenerator = gameInstanceGenerator;
            this.lobbyMessageSender = lobbyMessageSender;
            this.maxGames = maxGames;
        }

        public IEnumerable<TGame> GetAllGameInfo()
        {
            return gameInstances.Select(g => g.GameInfo); // ensure it's only a copy and not the original instance
        }

        public GameInstanceBase<TPlayer, TGame> GetGame(string gameId)
        {
            return gameInstances.FirstOrDefault(g => g.GameInfo.Id == gameId);
        }

        public void CreateGame(TGame gameInfo)
        {
            var instance = gameInstanceGenerator.CreateGameInstance(gameInfo);
            gameInstances.Add(instance);
            instance.OnStart();
            if(lobbyMessageSender != null)
                lobbyMessageSender.SendGameCreatedMessage(instance.GameInfo);
        }

        public void EndGame(string gameId)
        {
            var instance = GetGame(gameId);
            if (instance != null)
            {
                instance.OnStop();
                gameInstances.Remove(instance);
                if (lobbyMessageSender != null)
                    lobbyMessageSender.SendGameEndedMessage(instance.GameInfo);
            }
        }

        public void OnTick(double elapsedTime)
        {
            foreach (var instance in gameInstances)
                instance.OnTick(elapsedTime);
        }

        public TPlayer GetPlayer(string playerId)
        {
            return players.FirstOrDefault(p => p.Id == playerId);
        }

        public void AddPlayer(TPlayer playerInfo)
        {
            players.Add(playerInfo);
            if (lobbyMessageSender != null)
                playerInfo.AddToMessageGroup(MessageGroups.Lobby);
        }

        public void RemovePlayer(string playerId)
        {
            var player = GetPlayer(playerId);
            players.Remove(player);
            player.RemoveFromMessageGroup(MessageGroups.Lobby);
        }

        public void JoinGame(string playerId, string gameId)
        {
            var instance = GetGame(gameId);
            var player = GetPlayer(playerId);
            instance.AddPlayer(player);
            player.RemoveFromMessageGroup(MessageGroups.Lobby);
        }

        public void LeaveGame(string playerId, string gameId)
        {
            var instance = GetGame(gameId);
            var player = GetPlayer(playerId);
            instance.RemovePlayer(playerId);
            player.AddToMessageGroup(MessageGroups.Lobby);
        }
    }
}
