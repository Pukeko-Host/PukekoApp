using System;
using System.Collections.Generic;
using System.Text;

namespace PukekoApp.Models
{
    class User
    {
        public bool logged_in;
        public int Id;
        public int DiscordId;
        public string Username;
        public int Discriminator;
        public string Email;
        public string Avatar;
        public bool Verified; // Email verified
        public bool MFA; // Multi-factor authentication
        public string Locale;
        public decimal Balance;
    }
}
