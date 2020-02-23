using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using SqlKata;
using SqlKata.Compilers;
namespace App5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DersKayit : ContentPage
    {
        public DersKayit()
        {
            InitializeComponent();
            string[] branslar = new string[] { "Edebiyat", "Turkce", "Matematik", "Fizik", "Kimya", "Biyoloji", "Ingilizce" };
            foreach(string brans in branslar)
            {
                Brans.Items.Add(brans);
            }
            
        }

        void Button_invisible ()
        {
            string[] buttonnames = new string[] { "b105000", "b111000", "b130000", "b133000", "b140000", "b143000", "b170000", "b173000" };
            foreach(string bname in buttonnames)
            {
                Grid.FindByName<Button>(bname).IsVisible = false;
            }
        }

        void Brans_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string comm = $@"SELECT DersHocasi from Dersler WHERE DersAdi = '{Brans.SelectedItem.ToString()}'";
            Dictionary<string, List<string>> names = HelperFunctionss.Sqldeneme(comm);
            HelperFunctionss.Pickeradjuster(Hocalar, names,"DersHocasi");
            Gun.SelectedIndex = -1;     
            Gun.Items.Clear();
            Gun.IsEnabled = false;
            Grid.IsVisible = false;
        }
        void Hocalar_SelectedIndexChanged(object sender, System.EventArgs e)
        {
                if(Hocalar.SelectedIndex != -1)
            {
                
                string comm = $@"SELECT DersGünü from Dersler 
                WHERE DersAdi = '{Brans.SelectedItem.ToString()}' 
                and DersHocasi = '{Hocalar.SelectedItem.ToString()}'";
                Dictionary<string, List<string>> names = HelperFunctionss.Sqldeneme(comm);
                HelperFunctionss.Pickeradjuster(Gun, names, "DersGünü");
                Grid.IsVisible = false;
            }
        }
        void Gun_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Grid.IsVisible = false;
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
        void Button7_Clicked(object sender, System.EventArgs e)
        {
            Button_invisible();
            if(Brans.SelectedIndex != -1 && Hocalar.SelectedIndex != -1 && Gun.SelectedIndex != -1)
            {
                try
                {
                    string comm = $@"SELECT cast(date2 as time(0))[date2] from Dersler 
                    WHERE DersAdi = '{Brans.SelectedItem.ToString()}' 
                    and DersHocasi = '{Hocalar.SelectedItem.ToString()}' 
                    and DersGünü = '{Gun.SelectedItem.ToString()}'";
                    Dictionary<string, List<string>> table = HelperFunctionss.Sqldeneme(comm);
                    Person person1 = JsonConvert.DeserializeObject<Person>(Settings.GeneralSettings);
                    comm = $@"SELECT Personid from Kisiler where kullaniciadi = '{person1.Username}'";
                    string studid = HelperFunctionss.SqlExecuter(comm, 1);
                    comm = $@"SELECT cast(Dersler.date2 as time(0))[date2]
                              FROM Dersler
                              INNER JOIN derskayit ON Dersler.Ders_ID = derskayit.ders_id
                              where derskayit.student_id = '{studid}' AND Dersler.DersAdi = '{Brans.SelectedItem.ToString()}' 
                              and Dersler.DersHocasi = '{Hocalar.SelectedItem.ToString()}' 
                              and DersGünü = '{Gun.SelectedItem.ToString()}';";
                    Dictionary<string, List<string>> kayitlitimes = HelperFunctionss.Sqldeneme(comm);
                    
                    DersGunu.Text = Gun.SelectedItem.ToString();
                    Grid.IsVisible = true;

                    foreach(string row in table["date2"])
                    {
                        if(!(kayitlitimes["date2"].Contains(row)))
                        {
                            string bname = "b" + row.Replace(":", "");
                            try
                            {
                                Grid.FindByName<Button>(bname).IsVisible = true;
                            }
                            catch
                            {

                            }
                        }
                    }

                }

                catch(Exception exx)
                {
                    DisplayAlert("alert", exx.ToString(),"ok");
                }

            }
            else
            {
                DisplayAlert("Uyarı", "Lutfen butun seçenekleri eksiksiz seçiniz", "OK");
            }
            
        }
        void Button8_Clicked(object sender, System.EventArgs e)
        {
            
            try
            {
                Button btn = (Button)sender;
            string[] hours = new string[] { "10:50:00", "11:10:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "17:00:00", "17:30:00" };
           
            foreach (string bhour in hours)
            {
                string bname = "b" + bhour.Replace(":", "");
                if(btn == Grid.FindByName<Button>(bname))
                {
                        
                        string comm = $@"select Ders_ID from Dersler where DersAdi = '{Brans.SelectedItem.ToString()}'
                                        and DersHocasi = '{Hocalar.SelectedItem.ToString()}'
                                        and DersGünü = '{Gun.SelectedItem.ToString()}'
                                        and cast(date2 as time(0)) = '{bhour}'";
                        string names = HelperFunctionss.SqlExecuter(comm, 1);
                        Person person1 = JsonConvert.DeserializeObject<Person>(Settings.GeneralSettings);
                        comm = $@"select Personid from Kisiler where kullaniciadi = '{person1.Username}'";
                        string names2 = HelperFunctionss.SqlExecuter(comm, 1);
                        if (names != "null" && names2 != "null")
                        {
                            comm = $@"insert into derskayit where student_id = '{names2}' and ders_id = '{names}'";
                            string ret = HelperFunctionss.SqlExecuter(comm, 0);
                            if (ret == "null")
                            {
                                DisplayAlert("alert", "Zaten bu derse kayıt olmuşsunuz!", "ok");
                            }
                        }
                    
                        break;
                }
            }
            }
            catch (Exception exaa)
            {
                DisplayAlert("", exaa.Message + exaa.ToString(), "");
            }
            
        }
    }
}