using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFGGameServer.Core
{
    public abstract class GameInstanceBase<TPlayer, TGame> 
        where TPlayer : PlayerInfoBase
        where TGame : GameInfoBase
    {
        private TGame gameInfo;
        private List<TPlayer> players;
        private IMessageSender<TPlayer, TGame> messageSender;

        public GameInstanceBase(TGame gameInfo, IMessageSender<TPlayer, TGame> messageSender)
        {
            this.gameInfo = gameInfo;
            this.messageSender = messageSender;
            this.players = new List<TPlayer>();
            this.Status = GameStatus.Ready;
        }

        public TGame GameInfo { get { return gameInfo; } }
        public GameStatus Status { get; set; }
        public virtual void OnStart() {
            this.Status = GameStatus.Running;
        }
        public virtual void OnTick(double elapsedTime) { 
        }
        public virtual void OnStop()
        {
            this.Status = GameStatus.Stopped;
            foreach (var p in players)
                messageSender.SendGameEndMessage(p, gameInfo);
        }
        public void AddPlayer(TPlayer player)
        {
            if (players.Contains(player))
                throw new ArgumentException("Player already in game");
            messageSender.SendPlayerJoinedMessage(MessageGroups.All, player, gameInfo);
            players.Add(player);
        }
        public void RemovePlayer(string playerId)
        {
            var player = players.FirstOrDefault(p => p.Id == playerId);
            if (player == null)
                throw new ArgumentException("Player not in game");
            players.Remove(player);
            messageSender.SendPlayerLeftMessage(MessageGroups.All, player, gameInfo);
        }
        public void SendActionMessage(string messageGroup, string action, object parameters)
        {
            messageSender.SendActionMessage(messageGroup, gameInfo, action, parameters);
        }
        public void SendActionMessage(TPlayer player, string action, object parameters)
        {
            if (!players.Contains(player))
                throw new ArgumentException("Player not in game");
            messageSender.SendActionMessage(player, gameInfo, action, parameters);
        }
        public TPlayer GetPlayer(string id)
        {
            return players.FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<TPlayer> GetPlayersInMessageGroup(string messageGroup)
        {
            return players.Where(p => p.IsInMessageGroup(messageGroup));
        }
    }
}
