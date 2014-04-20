using LFGGameServer.Core;

namespace LFGGameServer.Samples.SignalR
{
    public class SamplePlayerInfo : PlayerInfoBase
    {
        public string Name { get; set; }

        public SamplePlayerInfo(string id, string name)
            : base(id)
        {
            this.Name = name;
        }
    }
}