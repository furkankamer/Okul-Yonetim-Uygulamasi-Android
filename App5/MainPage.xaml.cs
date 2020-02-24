using System;
using System.ComponentModel;
using Xamarin.Forms;
using Android.Net.Wifi;
using CheckBox = Xamarin.Forms.CheckBox;

namespace App5
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            if(Settings.GeneralSettings == string.Empty)
            {
                giriss.IsVisible = true;
                kayitt.IsVisible = true;
            }
            else
            {
                giriss.IsVisible = false;
                kayitt.IsVisible = false;
            }
        }
        void Button3_Clicked(object sender, System.EventArgs e)
        {
            App5.App.Current.MainPage = new Giris();
        }
        void Button4_Clicked(object sender, System.EventArgs e)
        {
            App5.App.Current.MainPage = new Kayitsayfasi();
        }
    }
}
