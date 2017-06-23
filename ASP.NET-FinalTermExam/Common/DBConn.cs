using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_FinalTermExam.Common
{
    public class DBConn
    {
        public static string GetDBConnection(string connName)
        {
            return System.Configuration.ConfigurationManager.
                ConnectionStrings[connName].ConnectionString.ToString();
        }
    }
}