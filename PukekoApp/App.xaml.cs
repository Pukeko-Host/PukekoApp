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
        public DBConnector DBConnector = new DBConnector();
        public User User;

        public App()
        {
            InitializeComponent();

            MainPage = new Login(this);
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