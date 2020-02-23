using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace App5
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            if(Settings.GeneralSettings == string.Empty)
            {

                MainPage = new MainPage();
                
            }
            else
            {
                
                MainPage = new LoggedIn();
                MainPage.DisplayAlert("Alert", Settings.GeneralSettings, "OK");
            }
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
