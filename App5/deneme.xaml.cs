﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace App5
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Deneme : ContentPage
    {
        public Deneme()
        {
            InitializeComponent();
            
        }


        void Button1_Clicked(object sender, System.EventArgs e)
        {
            
                string comm = $@"if exists(SELECT sifre from Kisiler WHERE kullaniciadi = '{username.Text}')  
                SELECT sifre from Kisiler WHERE kullaniciadi = '{username.Text}' 
                else
                select null";
                if (App5.HelperFunctionss.SqlExecuter(comm, 1) == "null")
                   {
                   DisplayAlert("Alert", "Yanlis Kullanici Adi", "OK");
                   username.Text = "";
                   pass.Text = "";
                   }
                else
                   {
                   if (App5.HelperFunctionss.SqlExecuter(comm, 1) != pass.Text)
                      {
                      DisplayAlert("Alert", "Yanlis Sifre", "OK");
                      }
                   else
                      {
                      
                    comm = $@"SELECT isim,soyisim,Sınıf from Kisiler WHERE kullaniciadi = '{username.Text}'";
                    Dictionary<string, List<string>> datas = HelperFunctionss.Sqldeneme(comm);
                    
                    try
                    {
                        Person person1 = new Person
                        {
                            Username = username.Text,
                            Name = datas["isim"][0],
                            Surname = datas["soyisim"][0],
                            Sinif = datas["Sınıf"][0]
                        };
                        Settings.GeneralSettings = JsonConvert.SerializeObject(person1);
                        App5.App.Current.MainPage = new LoggedIn();

                    }
                    catch(Exception exp)
                    {
                        DisplayAlert("Exception", exp.Message, "OK");
                    }
                }
                   }

        }
        

        }

}