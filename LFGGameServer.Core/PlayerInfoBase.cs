using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFGGameServer.Core
{
    public class PlayerInfoBase
    {
        private string id;
        private List<string> messageGroups = new List<string>();

        public PlayerInfoBase(string id)
        {
            this.id = id;
            messageGroups.Add(MessageGroups.All);
        }

        public string Id { get { return id; } }

        public bool IsInMessageGroup(string messageGroup)
        {
            return messageGroups.Contains(messageGroup);
        }

        public void AddToMessageGroup(string messageGroup)
        {
            if (!messageGroups.Contains(messageGroup))
                messageGroups.Add(messageGroup); 
        }

        public void RemoveFromMessageGroup(string messageGroup)
        {
            if (messageGroups.Contains(messageGroup))
                messageGroups.Remove(messageGroup);
        }
    }
}
