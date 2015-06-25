using System;

namespace OdooRpcWrapper
{
    public class OdooConnectionCredentials
    {
        #region Fields
        private const string _suffix_host_url = "xmlrpc";
        private const string _common_url = "common";
        private const string _object_url = "object";
        #endregion

        #region Properties
        public string ServerUrl { get; set; }
        public string DbName { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public int UserId { get; set; }
        public string CommonUrl
        {
            get
            {
                return String.Format("{0}/{1}/{2}", ServerUrl, _suffix_host_url, _common_url);
            }
        }

        public string ObjectUrl
        {
            get
            {
                return String.Format("{0}/{1}/{2}", ServerUrl, _suffix_host_url, _object_url);
            }
        }
        #endregion

        public OdooConnectionCredentials(string serverUrl, string dbName,
            string dbUser, string dbPassword)
        {
            this.ServerUrl = serverUrl;
            this.DbName = dbName;
            this.DbUser = dbUser;
            this.DbPassword = dbPassword;
        }        
    }
}
