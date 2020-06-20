using PukekoApp.Models;
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
        public bool GameserverVis = true;
        private App parent;

        public MainPage(App parentapp)
        {
            parent = parentapp;

            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            MainGrid.RaiseChild(Guilds);

            MainGrid.SizeChanged += SetNavSize;
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
                GameserverVis = false;
            }
            else
            {
                MainGrid.ColumnDefinitions[1].Width = 240;
                GameserverVis = true;
            }
        }
    }
}