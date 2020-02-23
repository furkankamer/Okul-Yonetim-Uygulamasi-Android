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
            Tarih.MinimumDate = DateTime.Now.AddDays((8 - DateTime.Today.DayOfWeek - DayOfWeek.Sunday));
            Tarih.MaximumDate = DateTime.Now.AddDays((8 - DateTime.Today.DayOfWeek - DayOfWeek.Sunday)).AddDays(5);
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
        }
        void Gun_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Saat.Items.Clear();
            Saat.SelectedItem = -1;
            string[,] hours = { { "14:30:00", "14:00:00", "13:00:00", "13:00:00" }, { "17:30:00", "17:00:00", "11:10:00", "10:50:00" } };
            if (Tarih.Date.DayOfWeek.ToString() == "Saturday") for (int i = 3; i >= 0; i--) { Saat.Items.Add(hours[0, i]); }
            else for (int i = 3; i >= 0; i--) { Saat.Items.Add(hours[1, i]); }
            Saat.IsEnabled = true;
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
        string Hangi_gun()
        {
            string gun = Tarih.Date.DayOfWeek.ToString();
            if (gun == "Monday") gun = "Pazartesi";
            else if (gun == "Tuesday") gun = "Salı";
            else if (gun == "Wednesday") gun = "Çarşamba";
            else if (gun == "Thursday") gun = "Perşembe";
            else if (gun == "Friday") gun = "Cuma";
            else if (gun == "Saturday") gun = "Cumartesi";
            return gun;
        }
        void Ders_Olustur(object sender, System.EventArgs e)
        {
            if(Hocalar.SelectedIndex != -1 && Brans.SelectedIndex != -1 && Tarih.Date.ToString() != "" && Saat.SelectedIndex != -1)
            {
                string Gun = Hangi_gun();
                string tarih = Tarih.Date.ToString("yyyy - MM - dd ");
                string hocaid = $"select Hoca_id from Hocalar where isim = '{Hocalar.SelectedItem.ToString()}'";
                hocaid = HelperFunctionss.SqlExecuter(hocaid, 2);
                string yenidersekle = $"insert into Dersler (DersGünü,DersAdi,date2,hoca_id,quota,enrolled)" +
                    $"values('{Gun}','{Brans.SelectedItem.ToString()}','{tarih}','{hocaid}','1','0')";
                if (HelperFunctionss.SqlExecuter(yenidersekle, 0) == "null")
                    DisplayAlert("Hata!", "Lütfen ders programını kontrol ediniz", "Tamam");
            }
            else
            {
                DisplayAlert("Uyarı", "Lütfen bütün seçenekleri seçtiğinizden emin olun.", "Tamam");
            }
        }
    }
}