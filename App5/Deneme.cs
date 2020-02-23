using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Xamarin.Forms;
using System.Data;
namespace App5
{
    class HelperFunctionss
    {
        static public void Pickeradjuster(Picker a, Dictionary<string, List<string>> datas,string key)
        {   
            a.SelectedIndex = -1;
            a.Items.Clear();
            foreach(string data in datas[key])
                {
                    a.IsEnabled = true;    
                    if(!(a.Items.Contains(data)))
                    {
                        a.Items.Add(data);
                    }
                }
        }


        public static Dictionary<string, List<string>> Sqldeneme(string commstring)
        {
            using (SqlConnection conne = new SqlConnection(Configuration.ConnectionString))
            {
                conne.Open();
                Dictionary<string, List<string>> mydict = new Dictionary<string, List<string>>();
                
                using (SqlCommand a = new SqlCommand(commstring, conne))
                {
                    using (SqlDataReader dataread = a.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(dataread);
                        for(int j=0;j<dt.Columns.Count;j++)
                        {
                            string colname = dt.Columns[j].ColumnName;
                            mydict[colname] = new List<string>();
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for(int j=0;j<dt.Columns.Count;j++)
                            {
                                string colname = dt.Columns[j].ColumnName;
                                mydict[colname].Add(dt.Rows[i][colname].ToString());
                            }
                            
                        }
                        return mydict;
                    }
                }
            }

        }

        public static string SqlExecuter(string commstring, int type1)
        {
            using (SqlConnection conne = new SqlConnection())
            {
                conne.ConnectionString = Configuration.ConnectionString;
                conne.Open();
            
            using (SqlCommand a = new SqlCommand(commstring, conne))
            {
                if (type1 == 0)
                {
                    try
                    {
                        a.ExecuteNonQuery();
                        return "";
                    }
                    catch
                    {
                        return "null";
                    }


                }

                else 
                {
                    try
                        {
                            var obj = a.ExecuteScalar();
                            if (obj == DBNull.Value)
                            {
                                return "null";
                            }
                            else
                            {
                                return obj.ToString();
                            }
                        }
                        catch(Exception exp)
                        {
                            App5.App.Current.MainPage.DisplayAlert("okk", exp.Message + "\n" + commstring, "okk");
                            return "null";
                        }
                    
                }

            }
        }
        }
    }
}
