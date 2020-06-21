using PukekoApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PukekoApp.ViewModels
{
    class Account
    {
        public string UserStatus
        {
            get { return $"{App.User.Username}#{App.User.Discriminator}"; }
        }
        public string Email
        {
            get { return App.User.Email; }
        }
    }
}
