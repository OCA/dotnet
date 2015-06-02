using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdooRpcWrapper
{
    public enum OdooLoginResult
    {
        Unknown,
        Success,
        InvalidCredentials,
        InvalidUri,
        InvalidDatabase
    }
}
