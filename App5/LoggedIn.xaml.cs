using Newtonsoft.Json;
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
            Person person = JsonConvert.DeserializeObject<Person>(Settings.GeneralSettings);
            if (person.Unvan == "Ogrenci") Kayit.IsVisible = true;
            else Olustur.IsVisible = true;
        }
        void DersKayit_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new DersKayit());
            Navigation.PushAsync(new NavigationPage(new DersKayit()));
        }
        void DersOlustur_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new DersOlustur());
            Navigation.PushAsync(new NavigationPage(new DersOlustur()));
        }
        void Button5_Clicked(object sender, System.EventArgs e)
        {
            Settings.GeneralSettings = string.Empty;
            Application.Current.MainPage = new MainPage();
        }
        

    }
}