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