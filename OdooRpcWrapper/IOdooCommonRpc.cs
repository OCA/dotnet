using CookComputing.XmlRpc;
using System;

namespace OdooRpcWrapper
{
    [XmlRpcUrl("common")]
    public interface IOdooCommonRpc : IXmlRpcProxy
    {
        [XmlRpcMethod("login")]
        object login(String dbName, string dbUser, string dbPwd);
    }
}
