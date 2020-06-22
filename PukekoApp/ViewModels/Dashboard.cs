using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using PukekoApp.Models;

namespace PukekoApp.ViewModels
{
    class Dashboard
    {
        public List<Guild> Guilds = App.User.Guilds;
        public string Status = "Loading...";
        public bool GameserverVis = true;

        public async Task Get()
        {
            var result = await App.APIConnector.ApiReq<List<Guild>>(Services.APIConnector.Method.GET, "account/guilds/");
            if(result.status == 200)
            {
                App.User.Guilds = result.obj;
                Guilds = App.User.Guilds;
                Status = "Game Servers";
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(title: "Failed to fetch guilds!", message: result.data, cancel: "Okay");
            }
        }
    }
}
