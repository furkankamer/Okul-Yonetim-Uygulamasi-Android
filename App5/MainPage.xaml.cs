using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Xamarin.Forms;
using Android.Net.Wifi;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Data.SqlClient;
using Switch = Xamarin.Forms.Switch;
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

        void Button_Clicked(object sender, System.EventArgs e)
        {
            if(ikili.IsChecked)
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
                else if(ikiden.IsChecked)
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
                a += Math.Round(miz).ToString() + "|";
                b += Math.Round(kil).ToString() + "|";
                foreach (double c in katsayilar)
                {
                    miz *= c;
                    kil *= c;
                    a += Math.Round(miz).ToString() + "|";
                    b += Math.Round(kil).ToString() + "|";
                }
                mizrak1.Text = a;
                kilic1.Text = b;
            }
            else if(uclu.IsChecked)
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
                a += Math.Round(miz).ToString() + "|";
                b += Math.Round(kil).ToString() + "|";
                foreach (double c in katsayilar)
                {
                    miz *= c;
                    kil *= c;
                    b += Math.Round(kil).ToString() + "|";
                    a += Math.Round(miz).ToString() + "|";
                }
                mizrak1.Text = a;
                kilic1.Text = b;
            }

            else if(dortlu.IsChecked)
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
                a += Math.Round(miz).ToString() + "|";
                b += Math.Round(kil).ToString() + "|";
                foreach (double c in katsayilar)
                {
                    miz *= c;
                    kil *= c;
                    b += Math.Round(kil).ToString() + "|";
                    a += Math.Round(miz).ToString() + "|";
                }
                mizrak1.Text = a;
                kilic1.Text = b;
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
            try
            {
                
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

            catch(Exception ex)
            {
                DisplayAlert("alert", ex.Message, "ok");
            }
            
        }
        void OnToggled1(object sender, ToggledEventArgs e)
        {
            if (!uclu.IsChecked) return;
            try
            {
                birden.IsVisible = false;
                ikiden.IsVisible = false;
                ucden.IsVisible = false;
                birden2.IsVisible = true;
                ikiden2.IsVisible = true;
                lbirden.IsVisible = false;
                likiden.IsVisible = false;
                lucden.IsVisible = false;
                lbirden2.IsVisible = true;
                likiden2.IsVisible = true;
                ikili.IsChecked = false;
                dortlu.IsChecked = false;
                birden2.IsChecked = false;
                ikiden2.IsChecked = false;
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
            if(swh == birden)
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
        void Button3_Clicked(object sender, System.EventArgs e)
        {
            App5.App.Current.MainPage = new Deneme();
           
        }
        void Button4_Clicked(object sender, System.EventArgs e)
        {
            App5.App.Current.MainPage = new Kayitsayfasi();

        }
        
    }
}
