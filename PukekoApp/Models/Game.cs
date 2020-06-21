using System;
using System.Collections.Generic;
using System.Text;

namespace PukekoApp.Models
{
    public class Game
    {
        public int Id;
        public string Name;
        public string Description;
        public string Perks;
        public string API;
        public string Background;
        public string Foreground;
        public string Icon;
        public List<Tier> Tiers;
        public List<GSMS> GSMSs;
    }
}
