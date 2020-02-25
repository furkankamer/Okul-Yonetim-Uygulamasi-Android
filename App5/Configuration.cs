using System;
using System.Collections.Generic;
using System.Text;

namespace App5
{
    public static class Configuration
    {
        //connection string..
#if DEBUG
        public const string ConnectionString = @"Data Source=denemedata.database.windows.net;User ID=djfurblood;Password=Kollama38;Initial Catalog = data;";
#else
        public const string ConnectionString = "release string";
#endif
    }
}
