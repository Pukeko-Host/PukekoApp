using System;
using System.Collections.Generic;
using System.Text;

namespace PukekoApp.Models
{
    public class Gameserver
    {
        public enum GameserverState { Active, Archived }

        public int Id;
        public int GameId;
        public Game Game;
        public int TierId;
        public Tier Tier;
        public int OwnerId;
        public int GuildId;
        public int ChatId;
        public string Name;
        public int Port;
        public bool Active;
        public bool Running;
        public int RemainingMinutes;
        public int CurrentPlayers;
        public int MaxPlayers;
        public DateTime LastPoll;
    }
}
