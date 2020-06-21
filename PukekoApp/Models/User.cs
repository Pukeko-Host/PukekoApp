using System;
using System.Collections.Generic;
using System.Text;

namespace PukekoApp.Models
{
    public class User
    {
        public bool logged_in = false;
        public int Id;
        public ulong DiscordId;
        public string Username;
        public int? Discriminator;
        public string Email;
        public string Avatar;
        public bool Verified; // Email verified
        public bool MFA; // Multi-factor authentication
        public string Locale;
        public decimal Balance;
        public List<Guild> Guilds;
    }
}
