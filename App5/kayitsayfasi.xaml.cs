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
        }
        void Button2_Clicked(object sender, System.EventArgs e)
        {
            string sqlcomm = $@"insert into Kisiler (kullaniciadi,sifre,isim,soyisim,mail,Sınıf)
                                values('{username.Text}','{pass.Text}','{isim.Text}','{soyisim.Text}','{email.Text}','{sinif.Text}');";

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
    }
}