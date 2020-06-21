﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PukekoApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Account : ContentPage
    {
        public Account()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = new ViewModels.Account();

            InitializeComponent();
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            // TODO
        }
    }
}