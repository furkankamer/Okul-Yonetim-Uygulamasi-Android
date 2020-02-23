using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoggedIn : ContentPage
    {
        public LoggedIn()
        {
            InitializeComponent();
        }
        void Button1_Clicked(object sender, System.EventArgs e)
        {
            App5.App.Current.MainPage = new NavigationPage(new DersKayit());
            Navigation.PushAsync(new NavigationPage(new DersKayit()));
        }
        void Button5_Clicked(object sender, System.EventArgs e)
        {
            Settings.GeneralSettings = string.Empty;
            App5.App.Current.MainPage = new MainPage();
        }
        

    }
}