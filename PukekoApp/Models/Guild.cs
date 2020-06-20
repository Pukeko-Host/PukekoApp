using System;
using System.Collections.Generic;
using System.Text;

namespace PukekoApp.Models
{
    class Guild
    {
        public int Id;
        public string Name;
        public string Icon;
        public int Pos;
        public int? FolderParent;
        public bool Valid;
        public Dictionary<Gameserver.GameserverState, Gameserver> Gameservers;
    }
}
