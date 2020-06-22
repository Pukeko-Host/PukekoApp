using PukekoApp.Models;
using PukekoApp.ViewModels;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PukekoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private Dashboard dashboard = new Dashboard();

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);

            BindingContext = dashboard;

            InitializeComponent();

            MainGrid.RaiseChild(Guilds);
            MainGrid.SizeChanged += SetNavSize;

            Task.Factory.StartNew(dashboard.Get);
        }

        private void SetNavSize(object sender, System.EventArgs e)
        {
            if(MainGrid.Width < 800)
            {
                MainGrid.ColumnDefinitions[2].Width = MainGrid.Width - 60;
            }
            else
            {
                MainGrid.ColumnDefinitions[2].Width = GridLength.Star;
            }
        }

        private void Trigger_PushNav(object sender, EventArgs e)
        {
            if (MainGrid.ColumnDefinitions[1].Width.Value > 0)
            {
                MainGrid.ColumnDefinitions[1].Width = 0;
                dashboard.GameserverVis = false;
            }
            else
            {
                MainGrid.ColumnDefinitions[1].Width = 240;
                dashboard.GameserverVis = true;
            }
        }
    }
}