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
            string[] branslar = new string[] { "Edebiyat", "Turkce", "Matematik", "Fizik", "Kimya", "Biyoloji", "Ingilizce" };
            foreach (string brans in branslar)
            {
                Brans.Items.Add(brans);
            }
        }

        void Brans_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string comm = $@"SELECT isim from Hocalar WHERE Brans = '{Brans.SelectedItem.ToString()}'";
            Dictionary<string, List<string>> names = HelperFunctionss.Sqlreaderexecuter(comm);
            HelperFunctionss.Pickeradjuster(Hocalar, names, "isim");
            Gun.SelectedIndex = -1;
            Gun.Items.Clear();
            Gun.IsEnabled = false;
        }
        void Hocalar_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (Hocalar.SelectedIndex != -1)
            {
                string[] gunler = {"Pazartesi","Salı","Çarşamba","Perşembe","Cuma","Cumartesi"};
                foreach (string gun in gunler) Gun.Items.Add(gun);
            }
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
        void Ders_Olustur(object sender, System.EventArgs e)
        {
            
        }
    }
}