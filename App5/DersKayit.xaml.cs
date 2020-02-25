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
            Grid.IsVisible = false;
            string[] buttonnames = new string[] { "b105000", "b111000", "b130000", "b133000", "b140000", "b143000", "b170000", "b173000" };
            foreach(string bname in buttonnames)
            {
                string lname = bname.Replace("b", "l");
                string rname = bname.Replace("b", "r");
                Grid.FindByName<Button>(bname).IsVisible = false;
                Grid.FindByName<Label>(lname).IsVisible = false;
                Grid.FindByName<RowDefinition>(rname).Height = 0;
            }
        }

        void Brans_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comm = $@"SELECT isim from Hocalar WHERE Brans = '{Brans.SelectedItem.ToString()}'";
            Dictionary<string, List<string>> names = HelperFunctionss.Sqlreaderexecuter(comm);
            HelperFunctionss.Pickeradjuster(Hocalar, names,"isim");
            Gun.SelectedIndex = -1;     
            Gun.Items.Clear();
            Gun.IsEnabled = false;
            Grid.IsVisible = false;
        }
        void Hocalar_SelectedIndexChanged(object sender, EventArgs e)
        {
                if(Hocalar.SelectedIndex != -1)
            {
                string hocaid = $"select Hoca_id from Hocalar where isim = '{Hocalar.SelectedItem.ToString()}'";
                hocaid = HelperFunctionss.SqlExecuter(hocaid, 1);
                string comm = $@"SELECT DersGünü from Dersler 
                WHERE DersAdi = '{Brans.SelectedItem.ToString()}' 
                and hoca_id = '{hocaid}' and enrolled != quota";
                Dictionary<string, List<string>> names = HelperFunctionss.Sqlreaderexecuter(comm);
                HelperFunctionss.Pickeradjuster(Gun, names, "DersGünü");
                Grid.IsVisible = false;
            }
        }
        void Gun_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid.IsVisible = false;
        }
            void Button5_Clicked(object sender, EventArgs e)
        {
            Settings.GeneralSettings = string.Empty;
            App5.App.Current.MainPage = new MainPage();
        }
        void Button6_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoggedIn();
        }
        void Button7_Clicked(object sender, EventArgs e)
        {
            Button_invisible();
            if(Brans.SelectedIndex != -1 && Hocalar.SelectedIndex != -1 && Gun.SelectedIndex != -1)
            {
                try
                {
                    string hocaid = $"select Hoca_id from Hocalar where isim = '{Hocalar.SelectedItem.ToString()}'";
                    hocaid = HelperFunctionss.SqlExecuter(hocaid, 1);
                    string comm = $@"SELECT cast(date2 as time(0))[date2] from Dersler 
                    WHERE DersAdi = '{Brans.SelectedItem.ToString()}' 
                    and hoca_id = '{hocaid}' 
                    and DersGünü = '{Gun.SelectedItem.ToString()}' and enrolled != quota";
                    Dictionary<string, List<string>> table = HelperFunctionss.Sqlreaderexecuter(comm);
                    Person person1 = JsonConvert.DeserializeObject<Person>(Settings.GeneralSettings);
                    comm = $@"SELECT cast(Dersler.date2 as time(0))[date2]
                              FROM Dersler
                              INNER JOIN derskayit ON Dersler.Ders_ID = derskayit.ders_id
                              where derskayit.student_id = '{person1.Id}' 
                              and DersGünü = '{Gun.SelectedItem.ToString()}';";
                    Dictionary<string, List<string>> kayitlitimes = HelperFunctionss.Sqlreaderexecuter(comm);

                    DersGunu.Text = Gun.SelectedItem.ToString();
                    foreach (string row in table["date2"])
                    {
                        string bname = "b" + row.Replace(":", "");
                        string lname = "l" + row.Replace(":", "");
                        string rname = "r" + row.Replace(":", "");
                        if (!(kayitlitimes["date2"].Contains(row)))
                        {
                            try
                            {
                                Grid.IsVisible = true;
                                Grid.FindByName<Button>(bname).IsVisible = true;
                                Grid.FindByName<Label>(lname).IsVisible = true;
                                Grid.FindByName<RowDefinition>(rname).Height = GridLength.Star;
                            }
                            catch
                            {

                            }
                        }
                    }
                    if (Grid.IsVisible == false)
                        DisplayAlert("Uyarı", "Kayıt olabileceğiniz Ders bulunmamaktır lütfen Ders Programınızı Kontrol Ediniz", "Tamam");
                }

                catch (Exception exx)
                {
                    DisplayAlert("alert", exx.ToString(), "ok");
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
                        string hocaid = $"select Hoca_id from Hocalar where isim = '{Hocalar.SelectedItem.ToString()}'";
                        hocaid = HelperFunctionss.SqlExecuter(hocaid, 1);
                        string comm = $@"select Ders_ID from Dersler where DersAdi = '{Brans.SelectedItem.ToString()}'
                                        and hoca_id = '{hocaid}'
                                        and DersGünü = '{Gun.SelectedItem.ToString()}'
                                        and cast(date2 as time(0)) = '{bhour}'";
                        string dersid = HelperFunctionss.SqlExecuter(comm, 1);
                        Person person1 = JsonConvert.DeserializeObject<Person>(Settings.GeneralSettings);
                        if (dersid != "null")
                        {
                            comm = $@"insert into derskayit(student_id,ders_id) values('{person1.Id}','{dersid}')
                                      update Dersler set enrolled = enrolled + 1 where Ders_ID = '{dersid}'";
                            string ret = HelperFunctionss.SqlExecuter(comm, 0);
                            if (ret == "null")
                            {
                                DisplayAlert("alert", "Zaten bu derse kayıt olmuşsunuz!", "ok");
                                return;
                            }
                            else
                            {
                                DisplayAlert("alert", "Ders Kaydı Başarılı!", "ok");
                            }
                        }
                        btn.IsVisible = false;
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