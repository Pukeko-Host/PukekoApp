using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PukekoApp.Services;
using PukekoApp.Views;
using PukekoApp.Models;

namespace PukekoApp
{
    public partial class App : Application
    {
        public static int StatusBarHeight = 0;
        public static DBConnector DBConnector;
        public static User User;

        public App()
        {
            DBConnector = new DBConnector();
            User = new User();

            InitializeComponent();
        }

        protected async override void OnStart()
        {
            var req = await DBConnector.ApiReq<User>(DBConnector.Method.GET, "account/");
            if (req.status == 200)
                User = req.obj;
            else
                Console.WriteLine(req.data);

            if (User.logged_in)
                MainPage = new NavigationPage(new MainPage());
            else
                MainPage = new NavigationPage(new Login());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async void Main_Clicked(object sender, EventArgs e)
        {
            await (Current.MainPage as NavigationPage).PushAsync(new MainPage());
        }

        private async void Account_Clicked(object sender, EventArgs e)
        {
            await (Current.MainPage as NavigationPage).PushAsync(new Account());
        }
    }
}