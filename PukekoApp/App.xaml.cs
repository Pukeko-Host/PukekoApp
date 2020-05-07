using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PukekoApp.Services;
using PukekoApp.Views;

namespace PukekoApp
{
    public partial class App : Application
    {
        public static int StatusBarHeight = 0;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<DBConnector>();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}