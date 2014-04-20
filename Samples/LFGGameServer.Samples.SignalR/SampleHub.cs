using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using LFGGameServer.Core;

namespace LFGGameServer.Samples.SignalR
{
    public class SampleHub : Hub
    {
        private static List<SampleGameInstance> gameInstances = new List<SampleGameInstance>();
        private static SignalRMessageSender messageSender = new SignalRMessageSender();

        public IEnumerable<SampleGameInfo> GetLobby()
        {
            return gameInstances.Select(g => g.GameInfo);
        }

        public string CreateGame(string gameName)
        {
            var gameInfo = new SampleGameInfo(Guid.NewGuid().ToString(), gameName);
            var gameInstance = new SampleGameInstance(gameInfo, messageSender);
            gameInstances.Add(gameInstance);
            gameInstance.OnStart();
            Clients.All.addGameToLobby(gameInfo);
            return gameInstance.GameInfo.Id;
        }

        public void EndGame(string gameId)
        {
            var gameInstance = gameInstances.FirstOrDefault(g => g.GameInfo.Id == gameId);
            if (gameInstance != null)
            {
                gameInstance.OnStop();
                gameInstances.Remove(gameInstance);
                Clients.All.removeGameFromLobby(gameInstance.GameInfo);
            }
        }

        public void JoinGame(string gameId, string playerName)
        {
           var playerInfo = new SamplePlayerInfo(Context.ConnectionId, playerName);
           var gameInstance = gameInstances.FirstOrDefault(g => g.GameInfo.Id == gameId);
           if (gameInstance != null)
           {
               Groups.Add(Context.ConnectionId, gameInstance.GameInfo.Id);
               gameInstance.AddPlayer(playerInfo);
           }
        }

        public void LeaveGame(string gameId)
        {
            var gameInstance = gameInstances.FirstOrDefault(g => g.GameInfo.Id == gameId);
            if (gameInstance != null)
            {
                Groups.Remove(Context.ConnectionId, gameInstance.GameInfo.Id);
                gameInstance.RemovePlayer(Context.ConnectionId);
            }
        }

        public SampleGameInfo GetGameInfo(string gameId)
        {
            var gameInstance = gameInstances.FirstOrDefault(g => g.GameInfo.Id == gameId);
            if(gameInstance != null) 
                return gameInstance.GameInfo;
            else 
                return null;
        }

        public IEnumerable<SamplePlayerInfo> GetGamePlayers(string gameId)
        {
            var gameInstance = gameInstances.FirstOrDefault(g => g.GameInfo.Id == gameId);
            if (gameInstance != null)
                return gameInstance.GetPlayersInMessageGroup(MessageGroups.All);
            else
                return null;
        }

        public SamplePlayerInfo GetPlayerInfo(string gameId, string playerId)
        {
            var gameInstance = gameInstances.FirstOrDefault(g => g.GameInfo.Id == gameId);
            if (gameInstance != null)
            {
                return gameInstance.GetPlayer(playerId);
            }
            else
                return null;
        }

        public void Poke(string gameId, string playerId)
        { 
            var gameInstance = gameInstances.FirstOrDefault(g => g.GameInfo.Id == gameId);
            if (gameInstance != null)
            {
                gameInstance.Poke(Context.ConnectionId, playerId);
            }
        }
    }
}