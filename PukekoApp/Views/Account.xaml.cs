using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PukekoApp.Models;

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

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            var result = await App.DBConnector.ApiReq<SysMsg>(Services.DBConnector.Method.GET, "acccount/logout/");
            if(result.status == 200)
            {
                App.DBConnector.Reset();
                await (App.Current.MainPage as NavigationPage).PushAsync(new Login());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(title: "Logout failed!", message: result.obj.desc, cancel: "Okay");
            }
        }
    }
}