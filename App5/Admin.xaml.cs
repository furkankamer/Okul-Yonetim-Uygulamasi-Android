using Android.Net.Wifi;
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
    public partial class Admin : ContentPage
    {
        public Admin()
        {
            InitializeComponent();
        }
        void Double_inserter(double[] katsayilar, double nmizrak, double nkilic, string mizrak, string kilic)
        {
            mizrak += Math.Round(nmizrak).ToString() + "|";
            kilic += Math.Round(nkilic).ToString() + "|";
            foreach (double c in katsayilar)
            {
                nmizrak *= c;
                nkilic *= c;
                mizrak += Math.Round(nmizrak).ToString() + "|";
                kilic += Math.Round(nkilic).ToString() + "|";
            }
            mizrak1.Text = mizrak;
            kilic1.Text = kilic;
        }
        void Button_Clicked(object sender, System.EventArgs e)
        {
            if (ikili.IsChecked)
            {
                string a = mizrak.Text.ToString();
                double miz = Int16.Parse(a);
                a = string.Empty;
                string b = kilic.Text.ToString();
                double kil = Int16.Parse(b);
                b = string.Empty;
                double[] katsayilar = new double[1];
                if (birden.IsChecked)
                {
                    miz /= 1.4;
                    kil /= 1.4;
                    katsayilar[0] = 0.4;
                }
                else if (ikiden.IsChecked)
                {
                    miz /= 1.5;
                    kil /= 1.5;
                    katsayilar[0] = 0.5;
                }
                else
                {
                    miz /= 1.6666;
                    kil /= 1.6666;
                    katsayilar[0] = 0.6666;
                }
                Double_inserter(katsayilar, miz, kil, a, b);
            }
            else if (uclu.IsChecked)
            {
                string a = mizrak.Text.ToString();
                double miz = Int16.Parse(a);
                a = string.Empty;
                string b = kilic.Text.ToString();
                double kil = Int16.Parse(b);
                b = string.Empty;
                double[] katsayilar = new double[2];
                if (birden2.IsChecked)
                {
                    miz /= 1.6;
                    kil /= 1.6;
                    katsayilar[0] = 0.4;
                    katsayilar[1] = 0.5;
                }
                else
                {
                    miz /= 1.8333;
                    kil /= 1.8333;
                    katsayilar[0] = 0.5;
                    katsayilar[1] = 0.333;
                }
                Double_inserter(katsayilar, miz, kil, a, b);
            }

            else if (dortlu.IsChecked)
            {
                double[] katsayilar = { 0.4, 0.5, 0.666 };
                string a = mizrak.Text.ToString();
                double miz = Int16.Parse(a);
                a = string.Empty;
                string b = kilic.Text.ToString();
                double kil = Int16.Parse(b);
                b = string.Empty;
                miz /= 1.732;
                kil /= 1.732;
                Double_inserter(katsayilar, miz, kil, a, b);
            }


        }
        void Button1_Clicked(object sender, System.EventArgs e)
        {
            if (Android.App.Application.Context.GetSystemService(Android.Content.Context.WifiService) is WifiManager wifiManager)
            {
                wifiManager.SetWifiEnabled(false);
            }
        }
        void Button2_Clicked(object sender, System.EventArgs e)
        {
            if (Android.App.Application.Context.GetSystemService(Android.Content.Context.WifiService) is WifiManager wifiManager)
            {
                wifiManager.SetWifiEnabled(true);
            }
        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
            if (!ikili.IsChecked) return;
            try {
                birden.IsVisible = true;
                ikiden.IsVisible = true;
                ucden.IsVisible = true;
                birden2.IsVisible = false;
                ikiden2.IsVisible = false;
                lbirden.IsVisible = true;
                likiden.IsVisible = true;
                lucden.IsVisible = true;
                lbirden2.IsVisible = false;
                likiden2.IsVisible = false;
                birden.IsChecked = false;
                ikiden.IsChecked = false;
                ucden.IsChecked = false;
                uclu.IsChecked = false;
                dortlu.IsChecked = false;
            }

            catch (Exception ex)
            {
                DisplayAlert("alert", ex.Message, "ok");
            }

        }
        void OnToggled1(object sender, ToggledEventArgs e)
        {
            if (!uclu.IsChecked) return;
            try
            {
                CheckBox[,] checks = { { ikili, dortlu, birden2, ikiden2 },{ birden, ikiden, ucden,null } };
                for (int i=0;i<4;i++) checks[0,i].IsChecked = false;
                for (int i = 0; i < 3; i++) checks[1, i].IsVisible = false;
                for (int i = 2; i < 4; i++) checks[0, i].IsVisible = true;
                Label[,] labels = { {lbirden,likiden,lucden }, {lbirden2,likiden2,null } };
                for (int i = 0; i < 3; i++) labels[0, i].IsVisible = false;
                for (int i = 0; i < 2; i++) labels[1, i].IsVisible = true;
            }

            catch (Exception ex)
            {
                DisplayAlert("alert", ex.Message, "ok");
            }
        }
        void OnToggled2(object sender, ToggledEventArgs e)
        {
            if (!dortlu.IsChecked) return;
            try
            {
                birden.IsChecked = false;
                ikiden.IsChecked = false;
                ucden.IsChecked = false;
                birden2.IsChecked = false;
                ikiden2.IsChecked = false;
                birden.IsVisible = false;
                ikiden.IsVisible = false;
                ucden.IsVisible = false;
                birden2.IsVisible = false;
                ikiden2.IsVisible = false;
                lbirden.IsVisible = false;
                likiden.IsVisible = false;
                lucden.IsVisible = false;
                lbirden2.IsVisible = false;
                likiden2.IsVisible = false;
                ikili.IsChecked = false;
                uclu.IsChecked = false;
            }

            catch (Exception ex)
            {
                DisplayAlert("alert", ex.Message, "ok");
            }
        }
        void OnToggled3(object sender, ToggledEventArgs e)
        {
            CheckBox swh = (CheckBox)sender;
            if (!swh.IsChecked) return;
            if (swh == birden)
            {
                ucden.IsChecked = false;
                ikiden.IsChecked = false;
            }
            else
            {
                ikiden2.IsChecked = false;
            }
        }
        void OnToggled4(object sender, ToggledEventArgs e)
        {
            CheckBox swh = (CheckBox)sender;
            if (!swh.IsChecked) return;
            if (swh == ikiden)
            {
                birden.IsChecked = false;
                ucden.IsChecked = false;
            }
            else
            {
                birden2.IsChecked = false;
            }
        }
        void OnToggled5(object sender, ToggledEventArgs e)
        {
            if (!ucden.IsChecked) return;
            ikiden.IsChecked = false;
            birden.IsChecked = false;
        }
        void Button5_Clicked(object sender, System.EventArgs e)
        {
            Settings.GeneralSettings = string.Empty;
            Application.Current.MainPage = new MainPage();
        }
    }
}