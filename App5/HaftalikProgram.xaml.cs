using Newtonsoft.Json;
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
    public partial class HaftalikProgram : ContentPage
    {
        public HaftalikProgram()
        {
            InitializeComponent();
            Program_Preparer();
        }

        void Program_Preparer()
        {
            try
            {
                Person person = JsonConvert.DeserializeObject<Person>(Settings.GeneralSettings);
                string dersids = $"select ders_id from derskayit where student_id = '{person.Id}'";
                Dictionary<string, List<string>> ders_ids = HelperFunctionss.Sqlreaderexecuter(dersids);
                foreach (string id in ders_ids["ders_id"])
                {
                    string ders = $"select DersGünü,DersAdi,hoca_id,cast(date2 as time(0))[time] from Dersler where Ders_ID = '{id}'";
                    Dictionary<string, List<string>> dersler = HelperFunctionss.Sqlreaderexecuter(ders);
                    string hocaisim = $"select isim from Hocalar where Hoca_id = '{dersler["hoca_id"][0]}'";
                    hocaisim = HelperFunctionss.SqlExecuter(hocaisim, 1);
                    Label label = new Label
                    {
                        Text = dersler["DersAdi"][0] + "\n" + hocaisim,
                    };
                    label.FontSize = Device.GetNamedSize(NamedSize.Micro, label);
                    string lname = dersler["time"][0].Replace(":", "");
                    lname = "l" + lname;
                    int row = Grid.GetRow(Grid.FindByName<Label>(lname));
                    int col = Grid.GetColumn(Grid.FindByName<Label>(dersler["DersGünü"][0]));
                    Grid.Children.Add(label, col, row);
                }
            }
            catch(Exception exp)
            {
                DisplayAlert("", exp.ToString(), "ok");
            }
            
        }
        void Button5_Clicked(object sender, System.EventArgs e)
        {
            Settings.GeneralSettings = string.Empty;
            Application.Current.MainPage = new MainPage();
        }
        void Button6_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoggedIn();
        }
    }
}