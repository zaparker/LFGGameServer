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
        private static ServerInstanceBase<SamplePlayerInfo, SampleGameInfo> gameServer = new ServerInstanceBase<SamplePlayerInfo, SampleGameInfo>(new SampleGameInstanceGenerator(), new SampleLobbyMessageSender());

        public SampleHub()
        {
            gameServer.Start();
        }

        public IEnumerable<SampleGameInfo> GetLobby()
        {
            return gameServer.GetAllGameInfo();
        }

        public void JoinServer(string name)
        {
            var playerInfo = new SamplePlayerInfo(Context.ConnectionId, name);
            gameServer.AddPlayer(playerInfo);
            Groups.Add(Context.ConnectionId, MessageGroups.Lobby);
        }

        public string CreateGame(string gameName, int maxPlayers)
        {
            var gameInfo = new SampleGameInfo(Guid.NewGuid().ToString(), maxPlayers, gameName);
            gameServer.CreateGame(gameInfo);
            return gameInfo.Id;
        }

        public void EndGame(string gameId)
        {
            gameServer.EndGame(gameId);
        }

        public void JoinGame(string gameId, string playerName)
        {
            gameServer.JoinGame(Context.ConnectionId, gameId);
            Groups.Add(Context.ConnectionId, gameId);
            Groups.Remove(Context.ConnectionId, MessageGroups.Lobby);
        }

        public void LeaveGame(string gameId)
        {
            gameServer.LeaveGame(Context.ConnectionId, gameId);
            Groups.Remove(Context.ConnectionId, gameId);
            Groups.Add(Context.ConnectionId, MessageGroups.Lobby);
        }

        public SampleGameInfo GetGameInfo(string gameId)
        {
            var gameInstance = gameServer.GetGame(gameId);
            if(gameInstance != null) 
                return gameInstance.GameInfo;
            else 
                return null;
        }

        public IEnumerable<SamplePlayerInfo> GetGamePlayers(string gameId)
        {
            var gameInstance = gameServer.GetGame(gameId);
            if (gameInstance != null)
                return gameInstance.GetPlayersInMessageGroup(MessageGroups.All);
            else
                return null;
        }

        public SamplePlayerInfo GetPlayerInfo(string playerId)
        {
            return gameServer.GetPlayer(playerId);
        }

        public void Poke(string gameId, string playerId)
        {
            var gameInstance = gameServer.GetGame(gameId) as SampleGameInstance;
            if (gameInstance != null)
            {
                gameInstance.Poke(Context.ConnectionId, playerId);
            }
        }
    }
}