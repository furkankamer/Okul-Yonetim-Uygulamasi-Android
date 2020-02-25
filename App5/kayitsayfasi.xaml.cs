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
    public partial class Kayitsayfasi : ContentPage
    {
        public Kayitsayfasi()
        {
            InitializeComponent();
            kullanicitipi.Items.Add("Ogrenci");
            kullanicitipi.Items.Add("Ogretmen");
            int[] siniflar = {9,10,11,12 };
            foreach (int i in siniflar) sinif.Items.Add(i.ToString());
        }
        void Button2_Clicked(object sender, System.EventArgs e)
        {
            if(pass.Text != pass2.Text)
            {
                DisplayAlert("Hata", "Şifreler uyuşmuyor lütfen kontrol ediniz", "OK");
                return;
            }
            else if (username.Text.Length < 6)
            {
                DisplayAlert("Hata", "kullanici adi 6 karakterden az olamaz", "OK");
                return;
            }
            else if(pass.Text.Length < 8)
            {
                DisplayAlert("Hata", "Şifre 8 karakterden az olamaz", "OK");
                return;
            }
            else if (email.Text.Length < 8)
            {
                DisplayAlert("Hata", "Email adresini kontrol ediniz", "OK");
                return;
            }
            else if (kullanicitipi.SelectedIndex == -1)
            {
                DisplayAlert("Hata", "Lütfen bir kullanici tipi seciniz", "OK");
                return;
            }
            string secilen_sinif = string.Empty;
            if (sinif.SelectedIndex != -1) secilen_sinif = sinif.SelectedItem.ToString();
            string sqlcomm = $@"insert into Kisiler (kullaniciadi,sifre,isim,soyisim,mail,Sınıf,unvan)
                                values('{username.Text}','{pass.Text}','{isim.Text}',
                                '{soyisim.Text}','{email.Text}','{secilen_sinif}',
                                '{kullanicitipi.SelectedItem.ToString()}');";

            if (App5.HelperFunctionss.SqlExecuter(sqlcomm, 0) != "null")
            {
                DisplayAlert("Alert", "Kayit Basarili", "OK");
                App5.App.Current.MainPage = new Giris();
            }
            else
            {
                DisplayAlert("Alert", "Kullanici adi veya Email Daha Once Kullanilmis", "OK");
                App5.App.Current.MainPage = new Kayitsayfasi();
            }
        }

        private void Kullanicitipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            kayito.IsEnabled = true;
            sinif.SelectedIndex = -1;
            if (kullanicitipi.SelectedItem.ToString() == "Ogrenci")
            {
                lsinif.IsVisible = true;
                sinif.IsVisible = true;
            }
            else
            {
                lsinif.IsVisible = false;
                sinif.IsVisible = false;
            }
        }
        private void Pass2_Completed(object sender, EventArgs e)
        {
            if (pass2.Text != pass.Text)
                DisplayAlert("Uyarı", "Şifreler uyuşmuyor lütfen doğru girdiğinizden emin olunuz", "Ok");
        }
    }
}