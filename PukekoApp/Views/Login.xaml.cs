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
        OAuth2Authenticator auth;
        OAuthLoginPresenter presenter;

        public bool canLogin = true;

        public Login()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
        }

        private void DiscordLogin_Clicked(object sender, EventArgs e)
        {
            canLogin = false;

            auth = new OAuth2Authenticator(
                clientId: "700549643045568522",
                scope: "identify email guilds",
                authorizeUrl: new Uri("https://discord.com/api/oauth2/authorize"),
                redirectUrl: new Uri("https://pukeko.yiays.com/app/account/callback/"),
                isUsingNativeUI: true
            );
            auth.Completed += auth_Completed;
            auth.Error += auth_Failed;

            presenter = new OAuthLoginPresenter();
            presenter.Login(auth);
        }
        private async void auth_Completed(object sender, AuthenticatorCompletedEventArgs eventArgs)
        {
            if (eventArgs.IsAuthenticated)
            {
                var apiauth = await App.DBConnector.ApiReq<SysMsg>(DBConnector.Method.POST, "account/app_login/", new Dictionary<string, object>() { { "access_token", eventArgs.Account.Properties["access_token"] } });
                if (apiauth.status == 200)
                {
                    var apiaccount = await App.DBConnector.ApiReq<User>(DBConnector.Method.GET, "account/");
                    if (apiaccount.obj.logged_in)
                    {
                        App.User = apiaccount.obj;
                        await (Application.Current.MainPage as NavigationPage).PushAsync(new MainPage());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(title: "Login failed!", message: "An unknown error occured.", cancel: "Okay");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(title: "Login failed!", message: $"Error {apiauth.status}: {apiauth.obj.desc}", cancel: "Okay");
                    canLogin = true;
                }

            }
            // Otherwise, the user is taken back to the login screen
            canLogin = true;
        }
        private async void auth_Failed(object sender, AuthenticatorErrorEventArgs eventArgs)
        {
            await Application.Current.MainPage.DisplayAlert(title: "Login failed!", message: eventArgs.Message, cancel: "Okay");
            canLogin = true;
        }
    }
}