using System;
using System.Collections.Generic;
using System.Text;

namespace PukekoApp.Models
{
    public class Guild
    {
        public ulong Id;
        public string Name;
        public string Icon;
        public int? Pos;
        public ulong? FolderParent;
        public bool Valid;
        public Dictionary<Gameserver.GameserverState, Gameserver> Gameservers;
    }
}
