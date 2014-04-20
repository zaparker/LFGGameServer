using LFGGameServer.Core;

namespace LFGGameServer.Samples.SignalR
{
    public class SampleGameInfo : GameInfoBase
    {
        public string Name { get; set; }

        public SampleGameInfo(string id, int maxPlayers, string name)
            : base(id, maxPlayers)
        {
            this.Name = name;
        }
    }
}