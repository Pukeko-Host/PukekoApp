using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PukekoApp.Services;
using PukekoApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;

namespace PukekoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private App parent;
        OAuth2Authenticator auth;

        public Login(App parentapp)
        {
            parent = parentapp;
            InitializeComponent();
        }

        private void DiscordLogin_Clicked(object sender, EventArgs e)
        {
            auth = new OAuth2Authenticator(
                clientId: "700549643045568522",
                scope: "identify email guilds",
                authorizeUrl: new Uri("https://discord.com/api/oauth2/authorize"),
                redirectUrl: new Uri("https://pukeko.yiays.com/app/account/callback/"),
                isUsingNativeUI: true
            );
            auth.Completed += async (authSender, authEventArgs) =>
            {
                if (authEventArgs.IsAuthenticated)
                {
                    var apiauth = await parent.DBConnector.ApiReq<SysMsg>(DBConnector.Method.POST, "account/app_login/", new Dictionary<string, string>() { { "access_token", authEventArgs.Account.Properties["access_token"] } });
                    if (apiauth.status == 200)
                    {
                        var apiaccount = await parent.DBConnector.ApiReq<User>(DBConnector.Method.GET, "account/");
                        parent.User = apiaccount.obj;
                        parent.MainPage = new MainPage(parent);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(title: "Login failed!", message: "Failed to login to the PukekoHost API", cancel: "Okay");
                    }

                }
                // Otherwise, the user is taken back to the login screen
            };
            auth.Error += async (authSender, authEventArgs) =>
            {
                await Application.Current.MainPage.DisplayAlert(title: "Login failed!", message: authEventArgs.Message, cancel: "Okay");
            };

            OAuthLoginPresenter presenter = new OAuthLoginPresenter();
            presenter.Login(auth);
        }
    }
}