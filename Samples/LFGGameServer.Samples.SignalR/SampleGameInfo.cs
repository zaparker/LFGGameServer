using LFGGameServer.Core;

namespace LFGGameServer.Samples.SignalR
{
    public class SampleGameInfo : GameInfoBase
    {
        public string Name { get; set; }

        public SampleGameInfo(string id, string name)
            : base(id)
        {
            this.Name = name;
        }
    }
}