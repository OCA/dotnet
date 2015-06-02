using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    /// <summary>
    /// Credentials for tests!!
    /// </summary>
    public class TestCreds
    {
        public static WebProxy Proxy = null;
        //public static WebProxy Proxy = new WebProxy("", true, null, new NetworkCredential("", ""));
        public static string Link = "http://2947826-19-98fcd3.runbot1.odoo-community.org";
        public static string Database = "2947826-19-98fcd3-all";
        public static string Username = "admin";
        public static string Password = "admin";
        
    }
}
