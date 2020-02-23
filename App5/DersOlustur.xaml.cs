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
    public partial class DersOlustur : ContentPage
    {
        public DersOlustur()
        {
            InitializeComponent();
        }
        void Button5_Clicked(object sender, System.EventArgs e)
        {
            Settings.GeneralSettings = string.Empty;
            App5.App.Current.MainPage = new MainPage();
        }
        void Button6_Clicked(object sender, System.EventArgs e)
        {
            App5.App.Current.MainPage = new LoggedIn();
        }
    }
}