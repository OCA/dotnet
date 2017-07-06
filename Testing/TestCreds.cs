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
    /// Change for local testing.
    /// </summary>
    public class TestCreds
    {
        public static WebProxy Proxy = null;
        public static string Link = "http://localhost:8069";
        public static string Database = "test";
        public static string Username = "admin";
        public static string Password = "admin";
    }
}
